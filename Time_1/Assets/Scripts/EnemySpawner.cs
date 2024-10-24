using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Referência ao prefab do inimigo (hexagono)
    public Transform spawnPoint; // Ponto de spawn do inimigo
    // por enquanto nao vamos tratar de waves
    public float spawnRate = 5f; // Intervalo de spawn do inimigo

    private float spawnCountdown; // Contagem regressiva para o spawn do inimigo
    // Update is called once per frame
    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown >= spawnRate) {
            SpawnEnemy(); // Spawn do inimigo
            spawnCountdown = 0f; // reinicia o contador
        }
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn do inimigo
    }
}
