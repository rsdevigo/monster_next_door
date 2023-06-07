using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class GameManager : Singleton<GameManager>
{
    public bool isPlayerTurn = true;
    private bool isEnemyMoving = false;
    public CODE_Enemy[] enemies;
    public bool equippedItem = false;
    public bool menuState = false;

    void Update()
    {
        if (isPlayerTurn || isEnemyMoving)
            return;

        StartCoroutine("MoveEnemies");
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
}
