using UnityEngine;
using UnityEngine.Events;

public class Object : MonoBehaviour
{
    private Rigidbody rb;
    private Collider mc;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<Collider>();
    }
    
    public GameObject PickUp()
    {
        Debug.Log("object-pickup");
        if (rb != null)
            rb.isKinematic = true;

        return gameObject;
    }
}