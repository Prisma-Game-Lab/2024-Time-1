using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Referência ao prefab do inimigo (hexagono)
    public Transform spawnPoint; // Ponto de spawn do inimigo
    public Transform enemyTarget;

    public TextMeshProUGUI wavesCountdownText; // Exibe cronômetro de preparação e waves
    
    public float timeBetweenWaves = 15f; // Tempo entre waves
    private float waveCountdown; // Contagem regressiva para waves  

    public float spawnRate = 1f; // Intervalo de spawn do inimigo
    public int initialEnemyAmount = 3;
    //private float spawnCountdown; // Contagem regressiva para o spawn do inimigo
    private int currentWave = 0;
    public int maxWaves = 10;

    private bool isPreparing = true;    // Indica se está na fase de preparação

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        StartCoroutine(PreparationPhase());
    }

    void Update()
    {
        // spawnCountdown += Time.deltaTime;
        // if (spawnCountdown >= spawnRate) {
        //     StartCoroutine(SpawnWaveCoroutine());// Spawn do inimigo
        //     spawnCountdown = 0f; // reinicia o contador
        //     contagem++;
        // }

        // if(contagem >= maxWaves)
        // {
        //     SceneManager.LoadScene("VictoryScene");
        // }
        if (isPreparing)
        {
            waveCountdown -= Time.deltaTime;
            wavesCountdownText.text = "Prepare-se: " + Mathf.Round(waveCountdown).ToString();
            if (waveCountdown <= 0f)
            {
                isPreparing = false;
                StartCoroutine(SpawnWaveCoroutine());
            }
        }
    }

    private IEnumerator PreparationPhase()
    {
        while (currentWave < maxWaves)
        {
            waveCountdown = timeBetweenWaves;
        yield return new WaitForSeconds(timeBetweenWaves);
        
        isPreparing = false;
        StartCoroutine(SpawnWaveCoroutine());

        yield return new WaitForSeconds(spawnRate * initialEnemyAmount);
        isPreparing = true;
        currentWave++;
        initialEnemyAmount += 2; // aumenta a quantidade inicial de inimigos a cada wave
        }
        // Após completar todas as waves, encerra o jogo
        SceneManager.LoadScene("VictoryScene");
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        wavesCountdownText.text = "Wave: " + (currentWave + 1).ToString();
        for (int i = 0; i < initialEnemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }
    void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn do inimigo
        enemy.GetComponent<EnemyMovement>().setTarget(enemyTarget);
    }
}
