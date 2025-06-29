using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; } = true;

    private Rigidbody rb;
    Collider mc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<Collider>();
    }
    public GameObject PickUp()
    {
        Debug.Log("pickableObject");
        if (rb != null)
            rb.isKinematic = true;

		return gameObject;
    }
}