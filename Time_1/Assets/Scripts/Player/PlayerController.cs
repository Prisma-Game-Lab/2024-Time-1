using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina = 10f;
    [SerializeField] private float staminaRegenRate = 0.5f;
    [SerializeField] private float staminaDecreaseRate = 0.5f;
    [SerializeField] private bool regenerated = true;
    [SerializeField] private float TimeToStartRegen = 1f;

    public Image StaminaBar;

    private float activeSpeed = 0f;
    private Color inicialBarColor;

    public Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        activeSpeed = moveSpeed;
        currentStamina = maxStamina;
        inicialBarColor = StaminaBar.color;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(currentStamina < 0) currentStamina = 0;
        StaminaBar.fillAmount = currentStamina / maxStamina;


        if(Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && regenerated == true) //Running 
        {
            activeSpeed = runSpeed;
            currentStamina -= staminaDecreaseRate * Time.deltaTime;

            if(currentStamina < 0)
            {
                regenerated = false;
                StaminaBar.color = Color.red;
                StartCoroutine(RechargeStamina());
            }
        }
        else //Walking
        { 
            activeSpeed = moveSpeed;

            if(currentStamina < maxStamina - 0.01 && regenerated == true)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(TimeToStartRegen);

        while(currentStamina < maxStamina - 0.01)
        {
            currentStamina += staminaRegenRate/10f;
            yield return new WaitForSeconds(.1f);
        }
        regenerated = true;
        StaminaBar.color = inicialBarColor;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * activeSpeed * Time.fixedDeltaTime);
    }
}
