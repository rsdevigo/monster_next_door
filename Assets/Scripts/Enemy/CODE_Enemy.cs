using SAP2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CODE_Enemy : CODE_MovingObject, CODE_IDamageable
{
    public Animator enemyAnimator;
    public CODE_Player target;
    public int power = 1;
    public bool isDead = false;
    [SerializeField]
    private SAP2DPathfindingConfig config;
    public Vector2[] path;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        isMoving = false;
     
    
    }
    private void Update()
    {
        //Debug.Log(isDead);
    }
    public void MoveEnemy()
    {
        path = SAP2DPathfinder.singleton.FindPath(transform.position, target.transform.position, config);
        if (path != null)
        {
            AttemptMove<CODE_IInteractive>(path[0]);
        }
    }

    protected override void OnCantMove<CODE_IInteractive>(CODE_IInteractive component)
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

    public void Damage(int targetLevel)
    {
        if (targetLevel >= power)
        {
            this.isDead = true;
            Debug.Log("Inimigo tomou dano");
        }
        else
        {
            target.isDead = true;
            Debug.Log("Player tomou dano");
        }
    }
}
