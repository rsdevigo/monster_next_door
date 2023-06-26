using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public bool isPlayerTurn = true;
    private bool isEnemyMoving = false;
    public CODE_Enemy[] enemies;
    public bool equippedItem = false;
    public bool menuState = false;
    public CODE_UIManager ui;
    
    void Update()
    {
        if (isPlayerTurn || isEnemyMoving)
            return;

        StartCoroutine("MoveEnemies");
        enemies = FindObjectsOfType<CODE_Enemy>();

    }
    private void Start()
    {
        ui.GetComponent<CODE_UIManager>();
        enemies = FindObjectsOfType<CODE_Enemy>();
    }
    private IEnumerator MoveEnemies(){
        isEnemyMoving = true;
        foreach(CODE_Enemy enemy in enemies) {
            if (!enemy.isDead) {
                enemy.MoveEnemy();
                yield return new WaitForSeconds(1.0f/enemy.moveSpeed);
            }
        }

        isEnemyMoving = false;
        isPlayerTurn = true;
    }
    public ItemCollected GetDisplayedItem()
    {
        // Retorna o item correspondente ao item exibido na interface
        ItemCollected[] items = FindObjectsOfType<ItemCollected>();
        foreach (ItemCollected item in items)
        {
            if (item.item.icon == ui.newItem.sprite)
            {
                return item;
            }
        }
        return null;
    }
    public void DestroyDisplayedItem()
    {
        // Encontra o item exibido na interface e o destroi
        ItemCollected item = GetDisplayedItem();
        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }

}
