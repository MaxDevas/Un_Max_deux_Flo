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
    }
    public GameObject PickUp()
    {
        Debug.Log(mc);
        if (rb != null)
            rb.isKinematic = true;


        return gameObject;
    }
}