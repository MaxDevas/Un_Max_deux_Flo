using UnityEngine;

public class MailletDestruction : MonoBehaviour, IInteractable
{
    private Item item;
    private Renderer renderer;
    private Collider collider;
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
        AudioManager.Instance.playSound(AudioManager.AudioType.Maillet, AudioManager.AudioSourceType.Player);
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
        collider.enabled = false;
        renderer.enabled = false;

    }
}
