using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask collisionLayer;
    public LayerMask grassLayer;
    public float moveSpeed;

    private bool isMoving;
    private Vector2 move;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (!isMoving)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            // remove diagonal movement
            if (move.x != 0) move.y = 0;

            if (move != Vector2.zero)
            {
                animator.SetFloat("moveX", move.x);
                animator.SetFloat("moveY", move.y);

                var newPos = transform.position;
                newPos.x += move.x;
                newPos.y += move.y;


                if (freeTile(new Vector3(newPos.x, newPos.y - 0.5f)))
                {
                    StartCoroutine(Move(newPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 newPos)
    {
        isMoving = true;

        while ((newPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = newPos;

        isMoving = false;

        checkEncounter();
    }

    private bool freeTile(Vector3 newPos)
    {
        return Physics2D.OverlapCircle(newPos, 0.1f, collisionLayer) == null;
    } 

    private void checkEncounter()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, grassLayer) != null)
        {
            //create  15% chance of wild encounter
            if (Random.Range(1, 101) <= 15){
                Debug.Log("encounter!");
            }
        }
    }
}