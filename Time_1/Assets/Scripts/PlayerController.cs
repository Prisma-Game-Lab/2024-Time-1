using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    private float activeSpeed = 0f;

    public Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        activeSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }
        else { activeSpeed = moveSpeed;}

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * activeSpeed * Time.fixedDeltaTime);
    }
}
