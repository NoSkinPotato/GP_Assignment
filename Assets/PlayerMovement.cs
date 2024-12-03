using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float movementSpeed;
    float horizontal, vertical;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private LogicManager manager;
    public float health = 10;
    public Slider healthBar;

    public bool onCow = false;

    bool onRadio = false;
    RadioScript rs;

    public static PlayerMovement Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        manager = LogicManager.Instance;
        Time.timeScale = 1;

        healthBar.maxValue = (int)health;
        healthBar.value = (int)health;
    }

    void Update()
    {

        if (onCow)
        {
            health -= Time.deltaTime;

            if (health <= 0)
            {
                LogicManager.Instance.EndGame(false);
            }
        }
        

        healthBar.value = (int)health;

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
        

        if (onRadio && Input.GetKeyDown(KeyCode.F))
        {
            rs.SetMusic();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Radio"))
        {
            rs = collision.gameObject.GetComponent<RadioScript>();
            rs.SetInput(true);
            onRadio = true;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Radio"))
        {
            rs.SetInput(false);
            onRadio = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            onCow = false;

        }
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
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            onCow = true;

        }
    
    }


}
