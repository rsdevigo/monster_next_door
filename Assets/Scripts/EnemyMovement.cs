using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask wallLayer;
    private bool isMoving;
    public Animator enemyAnimator;
    public PlayerMovement target;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && !GameManager.Instance.isPlayerTurn && !target.isMoving) {
            Vector3 dir = new Vector3(0f, 0f, 0f);
            if (Mathf.Abs(target.transform.position.x - transform.position.x) < 0.05f)
                dir.y = (target.transform.position.y > transform.position.y) ? 1f : -1f;
            else
                dir.x = (target.transform.position.x > transform.position.x) ? 1f : -1f;
            
            if (!Physics2D.Raycast(transform.position, dir, 1f, wallLayer)) {
                movePoint.position += dir;
                isMoving = true;
                StartCoroutine("move");
            } else {
                GameManager.Instance.isPlayerTurn = true;
            }
        }
    }

    public IEnumerator move() {
        while(Vector3.Distance(transform.position, movePoint.position) > 0.05f) {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Time.deltaTime*moveSpeed);
            yield return null;
        }
        isMoving = false;
        GameManager.Instance.isPlayerTurn = true;
    }


}
