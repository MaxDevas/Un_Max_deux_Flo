using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
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
			Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.isKinematic = false;
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
				//pickUpSource.Play();	//Mettre une source audio avant de réactiver ce code. ==> Son pour "attraper" un objet.
				inHandItem = pickableItem.PickUp();
				inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
				//inHandItem.transform.position = new Vector3(1f, 0f, 0.5f);
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

		/*if (inHandItem != null)
		{
			return;
		}*/

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
	}
}