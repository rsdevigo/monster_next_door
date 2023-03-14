using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door :MonoBehaviour, IInteractive
{
    public bool isOpen = false;
    [SerializeField]
    private Animator doorUpper;
    [SerializeField]
    private Animator doorLower;
    public void Interact()
    {
        isOpen = true;
        doorUpper.SetBool("isOpen", isOpen);
        doorLower.SetBool("isOpen", isOpen);
        StartCoroutine("Open");
    }

    IEnumerator Open(){
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
