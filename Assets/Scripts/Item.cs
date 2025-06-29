using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }
    private Rigidbody rb;
    private Collider mc;
    public bool pickedUp = false;

    public void Use(GameObject actor)
    {
        //actor.GetComponent<Player>().AddHealth(healthBoost);
        OnUse?.Invoke();
        //Destroy(gameObject);
    }

    public GameObject PickUp()
	{
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<Collider>();
        pickedUp = true;

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