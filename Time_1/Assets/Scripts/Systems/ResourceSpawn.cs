using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public static ResourceSpawn Instance { get; private set; }


    [Header("References")]
    public List<Transform> spawners = new List<Transform>();
    public GameObject eletronicPrefab;
    public GameObject metalPrefab;
    public GameObject prismPrefab;
    public GameObject uraniumPrefab;

    [Header("Probabilidades")]
    [Range(0f,100f)]public float[] probElectronic;
    [Range(0f, 100f)]public float[] probMetal;
    [Range(0f, 100f)]public float[] probPrism;
    [Range(0f, 100f)]public float[] probUranium;
    [Range(0f, 100f)]public float[] chanceSpawn;
    public int[] minimumSpawnEletronic;
    public int[] minimumSpawnMetal;
    public int[] minimumSpawnPrism;
    public int[] minimumSpawnUranium;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita múltiplas instâncias
        }
    }

    public void SpawnResources(int currentWave)
    {
        RemoveResources();
        List<Transform> spawnManipulation = new List<Transform>(spawners); //Crio uma copia de todos os possiveis spawns inicialmente
        minimumSpawn(spawnManipulation, currentWave); //Spawna o minimo de cada recurso
        SelectCurrentSpawners(spawnManipulation,currentWave);
        if(spawnManipulation.Count > 0)
        {
            SelectResource(spawnManipulation, currentWave);
        }
    }

    private void RemoveResources()
    {
        foreach (Transform spawn in spawners)
        {
            for (int i = spawn.childCount - 1; i >= 0; i--) 
            {
                Transform child = spawn.GetChild(i); 
                Destroy(child.gameObject); 
            }
        }
    }

    private void SelectResource(List<Transform> possibleSpawns, int currentWave)
    {
        List<float> probabilities = new List<float>(); 
        probabilities.Add(probElectronic[currentWave]); //Fazendo as probabilidades acumulativas
        probabilities.Add(probMetal[currentWave]);
        probabilities.Add(probPrism[currentWave]);
        probabilities.Add(probUranium[currentWave]);

        foreach(Transform spawner in possibleSpawns)
        {
            int chosenResource = -1;
            float RandomChance = Random.Range(0f, 100f);
            float cumulativeSum = 0f;
            for(int i = 0; i < probabilities.Count; i++)
            {
                cumulativeSum += probabilities[i];
                if(RandomChance < cumulativeSum)
                {
                    chosenResource = i;
                    break;
                }

            }

            if(chosenResource != -1)
            {
                switch(chosenResource)
                {
                    case 0:
                        Instantiate(eletronicPrefab, spawner);
                        break;
                    case 1:
                        Instantiate(metalPrefab, spawner);
                        break;
                    case 2:
                        Instantiate(prismPrefab, spawner);  
                        break;
                    case 3:
                        Instantiate(uraniumPrefab, spawner);
                        break;  
                    default:
                        break;
                }

            }
        }
    }
    private void minimumSpawn(List<Transform> possibleSpawns,int currentWave)
    {
        int randomIndex;
        if(minimumSpawnEletronic[currentWave] > 0) //Spawna o minimo de eletronico
        {
            for(int i = 0; i < minimumSpawnEletronic[currentWave]; i++)
            {
                randomIndex = Random.Range(0, possibleSpawns.Count);
                Transform currentSpawner = possibleSpawns[randomIndex];
                possibleSpawns.RemoveAt(randomIndex);
                Instantiate(eletronicPrefab, currentSpawner);
            }
        }

        if (minimumSpawnMetal[currentWave] > 0) //Spawna o minimo de metal
        {
            for (int i = 0; i < minimumSpawnMetal[currentWave]; i++)
            {
                randomIndex = Random.Range(0, possibleSpawns.Count);
                Transform currentSpawner = possibleSpawns[randomIndex];
                possibleSpawns.RemoveAt(randomIndex);
                Instantiate(metalPrefab, currentSpawner);
            }
        }

        if (minimumSpawnPrism[currentWave] > 0) //Spawna o minimo de prisma
        {
            for (int i = 0; i < minimumSpawnPrism[currentWave]; i++)
            {
                randomIndex = Random.Range(0, possibleSpawns.Count);
                Transform currentSpawner = possibleSpawns[randomIndex];
                possibleSpawns.RemoveAt(randomIndex);
                Instantiate(prismPrefab, currentSpawner);
            }
        }

        if (minimumSpawnUranium[currentWave] > 0) //Spawna o minimo de uranio
        {
            for (int i = 0; i < minimumSpawnUranium[currentWave]; i++)
            {
                randomIndex = Random.Range(0, possibleSpawns.Count);
                Transform currentSpawner = possibleSpawns[randomIndex];
                possibleSpawns.RemoveAt(randomIndex);
                Instantiate(uraniumPrefab, currentSpawner);
            }
        }

    }
    private void SelectCurrentSpawners(List<Transform> possibleSpawners,int currentWave) //Seleciona quais spawners vão acontecer
    {
        foreach(Transform spawner in possibleSpawners.ToArray())
        {
            float randomChance = Random.Range(0f, 100f);
            if(randomChance > chanceSpawn[currentWave])
            {
                possibleSpawners.Remove(spawner);
            }
        }          
    }
}
