using TMPro;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
	[SerializeField]
	private LayerMask pickableLayerMask;

	[SerializeField]
	private Transform playerCameraTransform;

	[SerializeField]
	private GameObject pickUpUI;

	internal void AddHealth(int healthBoost)
	{
		Debug.Log($"Health boosted by {healthBoost}");
	}

	[SerializeField]
	[Min(1)]
	private float hitRange = 3;

	[SerializeField]
	private Transform pickUpParent;

	[SerializeField]
	private GameObject inHandItem;

	/*[SerializeField]
	private InputActionReference interactionInput, dropInput, useInput;*/

	private RaycastHit hit;

	[SerializeField]
	private AudioSource pickUpSource;

	[SerializeField]
	private Collider collider;


	private void Use()
	{
		if (inHandItem != null)
		{
			IUsable usable = inHandItem.GetComponent<IUsable>();
			if (usable != null)
			{
				usable.Use(this.gameObject);
			}
		}
	}

	private void Drop()
	{
		if (inHandItem != null)
		{
			inHandItem.transform.SetParent(null);
			if (inHandItem.GetComponent<Object>())
			{
				inHandItem.GetComponent<Object>().pickedUp = false;
			}
			if (inHandItem.GetComponent<Item>())
			{
				inHandItem.GetComponent<Item>().pickedUp = false;
			}
			if (inHandItem.GetComponent<Weapon>())
			{
				inHandItem.GetComponent<Weapon>().pickedUp = false;
			}
			
			
			inHandItem = null;
			Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
			{
				rb.isKinematic = false;
				rb.GetComponent<Collider>().isTrigger = false;
			}
		}
	}

	private void PickUp()
	{
		if (hit.collider != null && inHandItem == null)
		{
			Item pickableItem = hit.collider.GetComponent<Item>();
			Object pickableObject = hit.collider.GetComponent<Object>();
			Weapon pickableWeapon = hit.collider.GetComponent<Weapon>();

			if (pickableItem != null)
			{
				inHandItem = pickableItem.PickUp();
				
				//inHandItem.transform.SetParent(pickUpParent.transform, false);

				inHandItem.transform.parent = pickUpParent;
				inHandItem.transform.localPosition = Vector3.zero;
			}
			else if (pickableWeapon != null)
			{
				//pickUpSource.Play();	//Mettre une source audio avant de r�activer ce code. ==> Son pour "attraper" un objet.
				inHandItem = pickableWeapon.PickUp();
				//inHandItem.transform.SetParent(pickUpParent.transform, true);

				inHandItem.transform.parent = pickUpParent;
				inHandItem.transform.localPosition = Vector3.zero;
			}
            else if (pickableObject != null)
            {
                inHandItem = pickableObject.PickUp();

                //inHandItem.transform.SetParent(pickUpParent.transform, false);

                inHandItem.transform.parent = pickUpParent;
            }

            //Debug.Log(hit.collider.name);
            //Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            //if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Weapon>())
            //{
            //    Debug.Log("It's food!");
            //    inHandItem = hit.collider.gameObject;
            //    inHandItem.transform.position = Vector3.zero;
            //    inHandItem.transform.rotation = Quaternion.identity;
            //    inHandItem.transform.SetParent(pickUpParent.transform, false);
            //    if(rb != null)
            //    {
            //        rb.isKinematic = true;
            //    }
            //    return;
            //}
            //if (hit.collider.GetComponent<Item>())
            //{
            //    Debug.Log("It's a useless item!");
            //    inHandItem = hit.collider.gameObject;
            //    inHandItem.transform.SetParent(pickUpParent.transform, true);
            //    if (rb != null)
            //    {
            //        rb.isKinematic = true;
            //    }
            //    return;
            //}
        }
	}

	private void Update()
	{
		Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
		if (hit.collider != null)
		{
			hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
			pickUpUI.SetActive(false);
		}	

		// input F
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (inHandItem == null)
			{
				PickUp();
			}
            else if (inHandItem != null)
            {
                Drop();
            }
        }

		if (inHandItem != null)
		{
			return;
		}

		if (Physics.Raycast(
			playerCameraTransform.position,
			playerCameraTransform.forward,
			out hit,
			hitRange,
			pickableLayerMask))
		{
			hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
			if (hit.collider.GetComponent<Weapon>())
			{
				var child = pickUpUI.transform.GetChild(0);
				child.GetComponent<TMP_Text>()?.SetText("C'est dangereux d'y aller seul !\nAppuie sur F pour prendre.");
			}
			else
			{
				var child = pickUpUI.transform.GetChild(0);
				child.GetComponent<TMP_Text>()?.SetText("Appuie sur F pour prendre.");
			}
			pickUpUI.SetActive(true);
		}

		
	}
}