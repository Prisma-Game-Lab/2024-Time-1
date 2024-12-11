using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Ponto de spawn do inimigo
    public Transform enemyTarget;   // Alvo dos inimigos
    public TextMeshProUGUI wavesCountdownText; // Exibe cronômetro de preparação e waves

    // Adicione esta linha para criar uma referência ao script RosaDosVentos
    public RosaDosVentos rosaDosVentos;

    public float timeBetweenWaves = 15f; // Tempo entre waves
    public float waveCountdown;         // Contagem regressiva para waves  
    private float searchCountdown = 1f;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        ResourceSpawn.Instance.SpawnResources(nextWave);
        waveCountdown = timeBetweenWaves;
        UpdateCountdownText();
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (state == SpawnState.COUNTING)
        {
            if (waveCountdown <= 0f)
            {
                StartCoroutine(SpawnWaveCoroutine(waves[nextWave]));
            }
            else
            {
                waveCountdown -= Time.deltaTime;
                UpdateCountdownText();
            }
        }

        void WaveCompleted()
        {
            Debug.Log("Wave Completed");
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;

            if (nextWave + 1 > waves.Length - 1)
            {
                nextWave = 0;
                Debug.Log("All Waves Completed");
                SceneManager.LoadScene("VictoryScene");
            }
            else
            {
                nextWave++;
                ResourceSpawn.Instance.SpawnResources(nextWave);
                UpdateCountdownText();
            }
        }

        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    IEnumerator SpawnWaveCoroutine(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;
        wavesCountdownText.text = "Wave: " + (nextWave + 1).ToString(); // Exibe a wave atual

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // Seleciona um ponto de spawn aleatório
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instancia o inimigo no ponto de spawn
        GameObject enemyInstance = Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation).gameObject;

        // Define o alvo do inimigo
        enemyInstance.GetComponent<EnemyMovement>().setTarget(enemyTarget);

        // Atualiza a direção ativa na rosa dos ventos
        if (rosaDosVentos != null)
        {
            rosaDosVentos.DirecaoInimigo(spawnIndex);
        }
        else
        {
            Debug.LogWarning("RosaDosVentos não está configurada no EnemySpawner.");
        }
    }

    void UpdateCountdownText()
    {
        if (state == SpawnState.COUNTING)
            wavesCountdownText.text = "Prepare-se: " + Mathf.Round(waveCountdown).ToString() + "s";
    }


}
