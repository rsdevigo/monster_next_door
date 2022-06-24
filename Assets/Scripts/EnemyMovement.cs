using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

public class EnemyMovement : MovingObject
{
    public Animator enemyAnimator;
    public PlayerMovement target;
    [SerializeField]
    private SAP2DPathfindingConfig config;
    public Vector2[] path;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        isMoving = false;
    }

    public void MoveEnemy()
    {
        path = SAP2DPathfinder.singleton.FindPath(transform.position, target.transform.position, config);
        Debug.Log(path);
        if (path != null) {
            AttemptMove<PlayerMovement>(path[0]);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Debug.Log("RELOU NO JOGADOR");
    }
}
