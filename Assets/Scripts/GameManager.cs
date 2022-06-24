using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
public class GameManager : Singleton<GameManager>
{
    public bool isPlayerTurn = true;
    private bool isEnemyMoving = false;
    public EnemyMovement[] enemies;

    void Update()
    {
        if (isPlayerTurn || isEnemyMoving)
            return;

        StartCoroutine("MoveEnemies");
    }

    private IEnumerator MoveEnemies(){
        isEnemyMoving = true;
        foreach(EnemyMovement enemy in enemies) {
            enemy.MoveEnemy();
            yield return new WaitForSeconds(1.0f/enemy.moveSpeed);
        }

        isEnemyMoving = false;
        isPlayerTurn = true;
    }
}
