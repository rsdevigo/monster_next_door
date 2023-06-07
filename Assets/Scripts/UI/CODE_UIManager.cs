using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CODE_UIManager : MonoBehaviour
{
    public GameObject panel;
    public Image equippedItem;
    public Image newItem;
    public bool desejatrocar = false;

  
    private void Start()
    {
        panel.SetActive(false);
    }
    public void OnChangedItem()
    {
        panel.SetActive(true);
      
    }
    public void GetImageEquippedItem(Sprite itemImage)
    {
        equippedItem.sprite = itemImage;
    }
    public void GetNewItemImage(Sprite itemImage)
    {
        newItem.sprite = itemImage;
    }
   
    public void YesChange()
    {
        panel.SetActive(false);
        desejatrocar = true;

        GameManager.Instance.menuState = false;
    }
   
  
    public void NoChange()
    {
        panel.SetActive(false);
        desejatrocar=false;

        GameManager.Instance.menuState = false;

    }


}
