using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Referência ao prefab do inimigo (hexagono)
    public Transform spawnPoint; // Ponto de spawn do inimigo
    public Transform enemyTarget;

    // por enquanto nao vamos tratar de waves
    public float spawnRate = 10f; // Intervalo de spawn do inimigo
    public int enemyAmount = 3;
    private float spawnCountdown; // Contagem regressiva para o spawn do inimigo
    private int contagem = 0;
    public int maxWaves = 10;

    private void Start()
    {
        spawnCountdown = 0f;
    }

    void Update()
    {
        spawnCountdown += Time.deltaTime;
        if (spawnCountdown >= spawnRate) {
            StartCoroutine(SpawnWaveCoroutine());// Spawn do inimigo
            spawnCountdown = 0f; // reinicia o contador
            contagem++;
        }

        if(contagem >= maxWaves)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }
    void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn do inimigo
        enemy.GetComponent<EnemyMovement>().setTarget(enemyTarget);
    }
}
