using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    private void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;  
    }

    void Die()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        
        Destroy(gameObject); // Destroi o inimigo
    }
}
