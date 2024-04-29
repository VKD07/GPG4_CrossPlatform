using AstarAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawners : MonoBehaviour
{
    public GameObject enemy;
    public GameObject ambulance;
    public Transform[] targetPoints;
    public Transform[] spawnPoints;
    public float delaySpawnTime = 3f;
    public float timeInterval = .5f;
    public int enemyPool = 20;
    public int ambulancePool = 50;
    public List<GameObject> spawnedCars;
    void Start()
    {
        SpawnPool();
        StartCoroutine(StartSpawningInLoop());
    }

    IEnumerator StartSpawningInLoop()
    {
        while (true)
        {
            ChooseAnEnemyToSpawn();
            yield return new WaitForSeconds(timeInterval);
        }
    }

    void ChooseAnEnemyToSpawn()
    {
        int randomIndex = Random.Range(0, spawnedCars.Count);
        GameObject carToSpawn = spawnedCars[randomIndex];

        if (!carToSpawn.activeSelf)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            int randomTarget = Random.Range(0, targetPoints.Length);
            carToSpawn.transform.position = spawnPoints[randomSpawn].position;
            carToSpawn.GetComponent<FindPath>().end = targetPoints[randomTarget];
            carToSpawn.SetActive(true);
            carToSpawn.GetComponent<FindPath>().FindNewPath();
        }
        else
        {
            for (int i = 0; i < spawnedCars.Count; i++)
            {
                if (!spawnedCars[i].activeSelf)
                {
                    carToSpawn = spawnedCars[i];
                    int randomSpawn = Random.Range(0, spawnPoints.Length);
                    int randomTarget = Random.Range(0, targetPoints.Length);
                    carToSpawn.transform.position = spawnPoints[randomSpawn].position;
                    carToSpawn.GetComponent<FindPath>().end = targetPoints[randomTarget];
                    carToSpawn.SetActive(true);
                    carToSpawn.GetComponent<FindPath>().FindNewPath();
                    break;
                }
            }
        }
    }

    void SpawnPool()
    {
        for (int i = 0; i < enemyPool; i++)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.SetActive(false);
            FindPath enemyPath = newEnemy.GetComponent<FindPath>();
            enemyPath.pathfindingManager = new PathThreadingManager();
            enemyPath.astar = FindObjectOfType<Astar>();
            int randomTarget = Random.Range(0, targetPoints.Length);
            enemyPath.end = targetPoints[randomTarget];
            spawnedCars.Add(newEnemy);
        }

        for (int i = 0; i < ambulancePool; i++)
        {
            GameObject newAmbulance = Instantiate(ambulance);
            newAmbulance.SetActive(false);
            FindPath ambulancePath = newAmbulance.GetComponent<FindPath>();
            ambulancePath.pathfindingManager = new PathThreadingManager();
            ambulancePath.astar = FindObjectOfType<Astar>();
            int randomTarget = Random.Range(0, targetPoints.Length);
            ambulancePath.end = targetPoints[randomTarget];
            spawnedCars.Add(newAmbulance);
        }
    }
}
