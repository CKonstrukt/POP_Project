using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    private int currentHealth;

    [Header("Damage")]
    public int damageToPlayer;

    [SerializeField] private bool isKnife;
    private GameObject enemySpawner;

    [Header("Death")]
    [SerializeField] private GameObject deathObject;
    [SerializeField] private float offsetX, offsetY, offsetZ;

    [Header("Sound")]
    [SerializeField] private AudioManager audioManager;


    void Start()
    {
        enemySpawner = GameObject.Find("+ Spawner");
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth <= 0)
        {
            audioManager.Play("EnemyDeath");

            currentHealth = maxHealth;
            enemySpawner.GetComponent<EnemySpawner>().LowerActiveEnemies(isKnife);

            // Spawn in the 'death' object on the current position, with possible offset.
            Vector3 spawnPos = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);
            Instantiate(deathObject, spawnPos, Quaternion.identity);

            if (isKnife)
            {
                transform.parent.gameObject.GetComponent<EnemyAI>().SetInactive();
            } else 
            {
                GetComponent<ForkHandler>().DestroyActiveProjectile();
                transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        Debug.Log("yo");
    }

}
