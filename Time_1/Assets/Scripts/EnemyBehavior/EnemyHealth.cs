using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    AudioManager audioManager;
    public string deathSound;

    public float health = 100f;

    public void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

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
        audioManager.PlaySound(deathSound);
        Destroy(gameObject); // Destroi o inimigo
    }
}
