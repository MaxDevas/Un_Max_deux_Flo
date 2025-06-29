using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IUsable
{
	[field: SerializeField]
	public UnityEvent OnUse { get; private set; }

	private Rigidbody rb;
    private Collider mc;

	public void Use(GameObject actor)
	{
		OnUse?.Invoke();
	}

	public GameObject PickUp()
	{
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<Collider>();

		if (rb != null)
		{
			rb.isKinematic = true;
		}

		if (mc != null)
		{
			mc.isTrigger = true;
		}
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;

		return this.gameObject;
	}
}