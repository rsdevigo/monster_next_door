using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door :MonoBehaviour, IInteractive
{
    public bool isOpen = false;
    public void Interact()
    {
        isOpen = true;
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
