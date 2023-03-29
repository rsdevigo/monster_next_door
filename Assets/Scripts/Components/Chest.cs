using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractive
{
    public Treasure treasure;
    private bool isOpen;


    private void Start()
    {
        isOpen = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interact();
    }

    public void Interact()
    {
        if (isOpen == false)
        {
            Debug.Log("ABRIR BAU");
            //Instantiate(treasureItem, this.transform.position, Quaternion.identity);
            isOpen = true;
        }
    }
}
