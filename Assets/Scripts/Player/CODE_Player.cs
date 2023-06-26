using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CODE_Player : CODE_MovingObject, CODE_IDamageable
{
    public CODE_UIManager ui;
    public Animator playerAnimator;
    public int level = 1;
    public int bonus = 0;
    public int currentLevel;
    public bool isDead;
    public delegate void ChangeItem();
    public event ChangeItem ChangedItem;
    public TextMeshProUGUI levelUI;

    // Start is called before the first frame update

    void Start()
    {
        movePoint.parent = null;
        isMoving = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            ui.Gameover();
            return;
        }
        currentLevel = level + bonus;
        levelUI.text = "Lv." + currentLevel.ToString();
        playerAnimator.SetBool("isMoving", isMoving);
        if (!GameManager.Instance.menuState)
        {
            if (!isMoving && GameManager.Instance.isPlayerTurn)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
                {
                    AttemptMove<CODE_IInteractive>(new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y));
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
                {
                    AttemptMove<CODE_IInteractive>(new Vector2(transform.position.x, transform.position.y + Input.GetAxisRaw("Vertical")));
                }
            }
        }
    }

    protected override void OnCantMove<CODE_IInteractive>(CODE_IInteractive component)
    {
        CODE_IDamageable damageable = component as CODE_IDamageable;
        if (damageable != null)
        {
            damageable.Damage(currentLevel);
        }
        else
        {
            component.Interact();
            if (GameManager.Instance.equippedItem == true)
            {

                if (ChangedItem != null)
                {
                    ChangedItem();
                }

            }
        }
        GameManager.Instance.isPlayerTurn = false;
    }

    protected override IEnumerator SmoothMove()
    {
        yield return base.SmoothMove();
        GameManager.Instance.isPlayerTurn = false;
    }

    public void Damage(int refPower)
    {
    }

    public void Interact()
    {

    }
}