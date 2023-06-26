using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollected : MonoBehaviour, CODE_IInteractive
{
    public CODE_Item_SO item;
    public CODE_Player player;
    public string nome;
    public int bonus;
    public CODE_UIManager ui;

    private void Start()
    {
        nome = item.nome;
        bonus = item.bonus;
        player = FindObjectOfType<CODE_Player>();
        ui = FindObjectOfType<CODE_UIManager>();
    }

    public void Interact()
    {
        if (player.bonus == 0)
        {
            player.bonus = this.item.bonus;
            ui.GetImageEquippedItem(this.item.icon, nome, bonus.ToString());

            Destroy(this.gameObject);
        }
        else
        {
            // faz com que chame o evento para mostrar a hud
            GameManager.Instance.equippedItem = true;
            ui.GetNewItemImage(this.item.icon, nome, bonus.ToString());
            GameManager.Instance.menuState = true;
            
        }
    }
    public void Trocar()
    {
        if (player.bonus != this.item.bonus) // Verifica se o item é diferente do item atualmente equipado
        {
            player.bonus = this.item.bonus;
            ui.GetImageEquippedItem(this.item.icon, nome, bonus.ToString());
            Destroy(this.gameObject);
            ui.desejatrocar = false;
        }
    }



}
