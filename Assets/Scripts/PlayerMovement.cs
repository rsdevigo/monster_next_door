using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovingObject
{
    public Animator playerAnimator;
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
                AttemptMove<EnemyMovement>(new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y ));
            }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1) {
               AttemptMove<EnemyMovement>(new Vector2(transform.position.x, transform.position.y + Input.GetAxisRaw("Vertical")));
            }
        }
    }

    protected override void OnCantMove<T> (T component) {
        Debug.Log("NÂO PODE SE MOVER");
        GameManager.Instance.isPlayerTurn = false;
    }

    protected override IEnumerator SmoothMove() {
        yield return base.SmoothMove();
        GameManager.Instance.isPlayerTurn = false;
    }
}
