using UnityEngine;

public class PlayerTriggerSpawn : MonoBehaviour
{
    [SerializeField] private byte spawnBoolIdx;
    [SerializeField] private EnemySpawner enemySpawner;


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            if (spawnBoolIdx == 0)
            {
                enemySpawner.canSpawn1 = true;
            } else {
                enemySpawner.canSpawn2 = true;
            }
            
            Destroy(gameObject);
        }
    }
}
