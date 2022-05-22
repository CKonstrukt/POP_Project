using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("wave Blockers")]
    [SerializeField] private List<GameObject> waveBlockers = new List<GameObject>();

    [Header("Spawning Enemies")]
    [SerializeField] private List<GameObject> knifeObject = new List<GameObject>();
    [SerializeField] private List<GameObject> knifeAndButterObject = new List<GameObject>();
    [SerializeField] private List<Transform> spawnLocations1 = new List<Transform>();
    [SerializeField] private List<Transform> spawnLocations2 = new List<Transform>();
    [HideInInspector] public bool canSpawn1, canSpawn2 = false;
    [HideInInspector] public int activeKnifeEnemies1, activeForkEnemies1 = 0;
    [HideInInspector] public int activeKnifeEnemies2, activeForkEnemies2 = 0;
    private int waveIdx1, waveIdx2 = 0;
    private int amountSpawned1, amountSpawned2 = 0;

    [Header("Other")]
    [SerializeField] private Transform player;

    private void Update() {
        if (canSpawn1)
        {
            switch (waveIdx1)
            {
                case 0:
                    waveBlockers[0].SetActive(true);
                    SpawnKnife(0, spawnLocations1);
                    waveIdx1 = 1;
                    break;
                case 1:
                    if (amountSpawned1 == 1 && activeKnifeEnemies1 == 0)
                    {
                        SpawnKnife(0, spawnLocations1);
                    } else if (amountSpawned1 == 2 && activeKnifeEnemies1 == 0)
                    {
                        SpawnKnife(0, spawnLocations1);
                        SpawnKnife(1, spawnLocations1);
                        waveIdx1 = 2;
                    }
                    break;
                case 2:
                    if (amountSpawned1 == 4 && activeKnifeEnemies1 == 0)
                    {
                        SpawnFork(0);
                        SpawnKnife(0, spawnLocations1);
                    } else if (amountSpawned1 == 6 && activeKnifeEnemies1 == 0)
                    {
                        SpawnKnife(0, spawnLocations1);
                        SpawnKnife(1, spawnLocations1);
                    } else if (amountSpawned1 == 8 && activeKnifeEnemies1 == 1)
                    {
                        SpawnFork(1);
                        SpawnKnife(2, spawnLocations1);
                        waveIdx1 = 3;
                    }
                    break;
                case 3:
                    if (activeForkEnemies1 == 0 && activeKnifeEnemies1 == 0)
                    {
                        waveBlockers[0].SetActive(false);
                        canSpawn1 = false;
                    }
                    break;
            }
        }

        if (canSpawn2)
        {
            switch (waveIdx2)
            {
                case 0:
                    waveBlockers[1].SetActive(true);
                    SpawnKnife(0, spawnLocations2);
                    SpawnKnife(1, spawnLocations2);
                    waveIdx2 = 1;
                    break;
                case 1:
                    if (amountSpawned2 == 2 && activeKnifeEnemies2 == 0)
                    {
                        SpawnFork(2);
                        SpawnKnife(0, spawnLocations2);
                    } else if (amountSpawned2 == 4 && activeKnifeEnemies2 == 0)
                    {
                        SpawnKnife(0, spawnLocations2);
                        SpawnKnife(1, spawnLocations2);
                    } else if (amountSpawned2 == 6 && activeKnifeEnemies2 == 1)
                    {
                        SpawnFork(3);
                        SpawnKnife(2, spawnLocations2);
                        waveIdx2 = 2;
                    }
                    break;
                case 2:
                    if (amountSpawned2 == 8 && activeForkEnemies2 == 0 && activeKnifeEnemies1 == 0)
                    {
                        SpawnFork(4);
                        SpawnFork(5);
                        SpawnFork(6);
                        SpawnKnife(0, spawnLocations2);
                    } else if (amountSpawned2 == 12 && activeKnifeEnemies1 == 0)
                    {
                        SpawnKnife(0, spawnLocations2);
                        SpawnKnife(1, spawnLocations2);
                        SpawnKnife(2, spawnLocations2);
                    } else if (amountSpawned2 == 15 && activeForkEnemies2 == 0 && activeKnifeEnemies1 <= 2)
                    {
                        SpawnKnife(3, spawnLocations2);
                        waveIdx2 = 3;
                    }
                    break;
                case 3:
                    if (activeForkEnemies2 == 0 && activeKnifeEnemies1 == 0)
                    {
                        waveBlockers[1].SetActive(false);
                        canSpawn2 = false;
                    }
                    break;
            }
        }
    }

    private void SpawnFork(int idx)
    {
        knifeAndButterObject[idx].SetActive(true);

        if (canSpawn1)
        {
            activeForkEnemies1++;
            amountSpawned1++;
        } else if (canSpawn2)
        {
            activeForkEnemies2++;
            amountSpawned2++;
        }
    }

    private void SpawnKnife(int idx, List<Transform> locations)
    {
        int randomPositionListIdx = Random.Range(0, locations.Count);
        if (Vector3.Distance(locations[randomPositionListIdx].position, player.position) < 2f)
        {
            randomPositionListIdx--;
            if (randomPositionListIdx == -1)
            {
                randomPositionListIdx = locations.Count - 1;
            }
        }

        knifeObject[idx].SetActive(true);
        knifeObject[idx].transform.position = locations[randomPositionListIdx].position;

        if (canSpawn1)
        {
            activeKnifeEnemies1++;
            amountSpawned1++;
        } else if (canSpawn2)
        {
            activeKnifeEnemies2++;
            amountSpawned2++;
        }
        
    }

    public void LowerActiveEnemies(bool isKnife)
    {
        if (canSpawn1)
        {
            if (isKnife)
            {
                activeKnifeEnemies1 -= 1;
                return;
            }
            activeForkEnemies1 -= 1;
        } else if (canSpawn2)
        {
            if (isKnife)
            {
                activeKnifeEnemies2 -= 1;
                return;
            }
            activeForkEnemies2 -= 1;
        }
    }
}
