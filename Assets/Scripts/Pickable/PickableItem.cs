using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
	[field: SerializeField]
	public bool KeepWorldPosition { get; private set; }

	private Rigidbody rb;
    Collider mc;

    private void Awake()
	{
		rb = GetComponent<Rigidbody>();
        mc = GetComponent<Collider>();
    }

	public GameObject PickUp()
	{
		if (rb != null)
		{
			rb.isKinematic = true;
		}

        //if (mc != null)
        //{
        //    mc.isTrigger = true;
        //}
        transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;

		return this.gameObject;
	}
}