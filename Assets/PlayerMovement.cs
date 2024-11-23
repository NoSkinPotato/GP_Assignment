using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float movementSpeed;
    float horizontal, vertical;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private LogicManager manager;

    private void Start()
    {
        manager = LogicManager.Instance;
        Time.timeScale = 1;
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }


        animator.SetInteger("Horizontal", (int)horizontal);
        animator.SetInteger("Vertical", (int)vertical);

        if (animator.GetBool("Attack") == false) {
            switchSides();
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(horizontal, vertical).normalized * Time.fixedDeltaTime * movementSpeed);
    }


    private void switchSides()
    {
        if(spriteRenderer.flipX && horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }else if (!spriteRenderer.flipX && horizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Attack()
    {
        if(animator.GetBool("Attack") == false)
        {
            animator.SetBool("Attack", true);
        }
    }

    public void StopAttacking()
    {
        animator.SetBool("Attack", false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            PlantScript script = collision.gameObject.GetComponent<PlantScript>();

            if (script.hitToDie > 0)
            {
                script.hitToDie--;
            }
            else
            {
                switch (script.plantType)
                {
                    case PlantType.wheat:
                        manager.wheat++;
                        break;
                    case PlantType.tomato:
                        manager.tomato++;
                        break;
                }
                manager.UpdateCount();

                Destroy(script.gameObject);
            }
        }
    }
}
