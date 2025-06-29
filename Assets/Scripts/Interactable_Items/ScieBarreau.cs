using UnityEngine;

public class ScieBarreau : MonoBehaviour, IInteractable
{
    private Item item;
    private Rigidbody rb;
    // Start is called once be;fore the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Interact()
    {
        Debug.Log("Hey");
        AudioManager.Instance.playSound(AudioManager.AudioType.Scie, AudioManager.AudioSourceType.Player);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

    }
}
