using UnityEngine;

public class ClePorte : MonoBehaviour, IInteractable
{
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        Debug.Log("Hey");
        _animator.SetTrigger("Open");

    }
}
