using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

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
			inHandItem = null;
			Debug.Log(hit);
			Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            Debug.Log(hit);
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
			IPickable pickableItem = hit.collider.GetComponent<IPickable>();
			if (pickableItem != null)
			{
				if (hit.collider.GetComponent<Weapon>())
				{
					//pickUpSource.Play();	//Mettre une source audio avant de réactiver ce code. ==> Son pour "attraper" un objet.
					inHandItem = pickableItem.PickUp();
					//inHandItem.transform.SetParent(pickUpParent.transform, true);

                    Debug.Log("HzzzEy");

					inHandItem.transform.parent = pickUpParent;
					inHandItem.transform.localPosition = Vector3.zero;

					//inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
					//inHandItem.transform.SetPositionAndRotation(pickUpParent.position, pickUpParent.rotation);
					/*Vector3 mini_deplacement = new Vector3(0.4f, -0.31f, 0.63f);
					Vector3 mini_rotation = new Vector3(1.33f, 97.85f, -5.19f);
					inHandItem.transform.position = inHandItem.transform.position + mini_deplacement;
					inHandItem.transform.rotation = inHandItem.transform.rotation * Quaternion.Euler(mini_rotation);*/

				}
                else if (hit.collider.GetComponent<Item>())
				{
					Debug.Log(pickableItem);
                    inHandItem = pickableItem.PickUp();

                    Debug.Log("HEy");
                    //inHandItem.transform.SetParent(pickUpParent.transform, false);

                    Debug.Log("HEy");

					inHandItem.transform.parent = pickUpParent;
					//inHandItem.transform.localPosition = Vector3.zero;
				}

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
			Debug.Log(hit);
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
			pickUpUI.SetActive(true);
		}

		
	}
}