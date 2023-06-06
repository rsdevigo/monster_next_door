using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour, CODE_IInteractive
{
    public CODE_Item_SO item;
    public CODE_Player player;

    public string nome;
    public int bonus;
    public Sprite icon;
  
    private void Start()
    {
        nome= item.nome;
        bonus= item.bonus;
        icon= item.icon;
    }

    public void Interact()
    {
        if (player.bonus == 0)
        {
            player.bonus = this.item.bonus;
            Destroy(this.gameObject);
        }
        else
        {
            // Adicionar o Canvas
            Debug.Log("deseja trocar?");
        }
    }
}
