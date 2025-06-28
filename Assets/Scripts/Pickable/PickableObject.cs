using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; } = true;

    Rigidbody rb;
    Collider mc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //mc = GetComponent<Collider>();
    }
    public GameObject PickUp()
    {
        if (rb != null)
            rb.isKinematic = true;
        //if (mc != null)
        //    mc.isTrigger = true;

        mc.enabled = false;

        return gameObject;
    }
}