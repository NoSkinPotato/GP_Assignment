using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    private PlayerMovement player;
    [SerializeField] private Rigidbody2D rb;
    public float movementSpeed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.Instance;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (player.transform.position - rb.transform.position).normalized;
        rb.transform.position += (Vector3)direction * movementSpeed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (player.transform.position.x < rb.transform.position.x && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }else if (player.transform.position.x >= rb.transform.position.x && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
    }

}
