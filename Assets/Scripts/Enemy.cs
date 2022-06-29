using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

public class Enemy : MovingObject, IDamageable
{
    public Animator enemyAnimator;
    public Player target;
    [SerializeField]
    private SAP2DPathfindingConfig config;
    public Vector2[] path;
    public int power = 1;
    public bool isDead = false;
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
            AttemptMove<IInteractive>(path[0]);
        }
    }

    protected override void OnCantMove<IInteractive>(IInteractive component)
    {
        IDamageable damageable = component as IDamageable;
        if (damageable != null) {
            damageable.Damage(power);
        } else {
            component.Interact();
        }
    }

    public void Interact()
    {
        Debug.Log("Interagiu com o Inimigo");
    }

    public void Damage(int power)
    {
        Debug.Log("Atacou o Inimigo");
    }
}
