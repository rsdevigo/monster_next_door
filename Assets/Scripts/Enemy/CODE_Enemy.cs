using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CODE_Enemy : CODE_MovingObject, CODE_IDamageable
{
    public Animator enemyAnimator;
    public CODE_Player target;
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
        if (path != null)
        {
            AttemptMove<CODE_IInteractive>(path[0]);
        }
    }

    protected override void OnCantMove<IInteractive>(CODE_IInteractive component)
    {
        CODE_IDamageable damageable = component as CODE_IDamageable;
        if (damageable != null)
        {
            damageable.Damage(power);
        }
        else
        {
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