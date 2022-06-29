using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject, IDamageable
{
    public Animator playerAnimator;
    public int level = 1;
    public int bonus = 0;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("isMoving", isMoving);
        if (!isMoving && GameManager.Instance.isPlayerTurn) {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1) {
                AttemptMove<IInteractive>(new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y ));
            }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1) {
               AttemptMove<IInteractive>(new Vector2(transform.position.x, transform.position.y + Input.GetAxisRaw("Vertical")));
            }
        }
    }

    protected override void OnCantMove<IInteractive>(IInteractive component)
    {
        IDamageable damageable = component as IDamageable;
        if (damageable != null) {
            damageable.Damage(level + bonus);
        } else {
            component.Interact();
        }
        GameManager.Instance.isPlayerTurn = false;   
    }

    protected override IEnumerator SmoothMove() {
        yield return base.SmoothMove();
        GameManager.Instance.isPlayerTurn = false;
    }

    public void Interact()
    {
        Debug.Log("Interagiu com o Jogador");
    }

    public void Damage(int power)
    {
        Debug.Log("Atacou o Jogador");
    }
}
