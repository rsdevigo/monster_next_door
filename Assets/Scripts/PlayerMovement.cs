using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask wallLayer;
    public bool isMoving;

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
        if (!isMoving && GameManager.Instance.isPlayerTurn) {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1) {
                if (!Physics2D.Raycast(transform.position, new Vector2(Input.GetAxisRaw("Horizontal"), 0f), 1f, wallLayer)) {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    isMoving = true;
                    StartCoroutine("move");
                }
            }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1) {
                if (!Physics2D.Raycast(transform.position, new Vector2(0f, Input.GetAxisRaw("Vertical")), 1f, wallLayer)) {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    isMoving = true;
                    StartCoroutine("move");
                }
            }
        }
        playerAnimator.SetBool("isMoving", isMoving);
    }

    public IEnumerator move() {
        while(Vector3.Distance(transform.position, movePoint.position) > 0.05f) {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Time.deltaTime*moveSpeed);
            yield return null;
        }
        isMoving = false;
        GameManager.Instance.isPlayerTurn = false;
    }
}
