using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    protected bool isMoving;
    [SerializeField]
    protected Transform movePoint;
    public LayerMask blockingLayer;
    public float moveSpeed = 5.0f;

    public virtual void AttemptMove <T> (Vector2 position) where T : IInteractive {
        RaycastHit2D hit;
        bool canMove = Move(position, out hit);
        if (hit.transform == null)
            return;

        T obj = hit.transform.GetComponent<T>();

        if (!canMove && obj != null) {
            OnCantMove<T>(obj);
        }
    }

    protected virtual bool Move(Vector2 position, out RaycastHit2D hit) {
        hit = Physics2D.Linecast(transform.position, position, blockingLayer);
        if (hit.transform == null) {
            isMoving = true;
            movePoint.position = position;
            StartCoroutine("SmoothMove");
            return true;
        }
        return false;
    }

    protected virtual IEnumerator SmoothMove()
    {
        while(Vector2.Distance(transform.position, movePoint.position) > float.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Time.deltaTime*moveSpeed);
            yield return null;
        }
        isMoving = false;
    }

    protected abstract void OnCantMove <T> (T component) where T : IInteractive;
}
