using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour
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
    public void GetItem( CODE_Player player)
    {
        player.bonus = item.bonus;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player"))
        {
            GetItem(player);
            Destroy(this.gameObject);
        }
     
    }



}
