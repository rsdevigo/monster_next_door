using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour, CODE_IInteractive
{
    public CODE_Item_SO item;
    public CODE_Player player;

    public string nome;
    public int bonus;
    public SpriteRenderer icon;
    public CODE_UIManager ui;
  
    private void Start()
    {
        nome= item.nome;
        bonus= item.bonus;
        icon.sprite = item.icon;
    }

    public void Interact()
    {
        if (player.bonus == 0)
        {
            player.bonus = this.item.bonus;
            ui.GetImageEquippedItem(item.icon);
            
            Destroy(this.gameObject);
        }
        else
        {
            // faz com que chame o evento para mostrar a hud
            GameManager.Instance.equippedItem = true;
            ui.GetNewItemImage(item.icon);

            if(ui.desejatrocar==true)
            {
                player.bonus = this.item.bonus;
                ui.GetImageEquippedItem(item.icon);

                Destroy(this.gameObject);

            }
            
        
        }
    }


   
}
