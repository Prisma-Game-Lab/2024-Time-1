using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina = 10f;
    [SerializeField] private float staminaRegenRate = 0.5f;
    [SerializeField] private float staminaDecreaseRate = 0.5f;
    [SerializeField] private bool regenerating = false;
    [SerializeField] private bool regenerated = true;

    private float activeSpeed = 0f;

    public Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        activeSpeed = moveSpeed;
        currentStamina = maxStamina;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && regenerated) //Running 
        {
            activeSpeed = runSpeed;
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            if(currentStamina < 0)
            {
                regenerating = true;
                regenerated = false;
            }
        }
        else //Walking
        { 
            activeSpeed = moveSpeed;
            if(currentStamina < maxStamina - 0.01 && regenerating == true)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
            else if (currentStamina >= maxStamina - 0.01 && regenerating == true)
            {
                regenerating = false;
                regenerated = true;
            }
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * activeSpeed * Time.fixedDeltaTime);
    }
}
