using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Vector3 dir;

    [SerializeField] private int bulletDamage;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float destroyTime;
    private float timeAtSpawn;

    private GameObject enemyToTrack;

    void Start()
    {
        timeAtSpawn = Time.time;
    }


    void Update()
    {
        if (Time.time - timeAtSpawn >= destroyTime)
        {
            Destroy(gameObject);
        }

        if (enemyToTrack != null)
        {
            Vector3 targetPos = new Vector3(enemyToTrack.transform.position.x, transform.position.y, enemyToTrack.transform.position.z);

            float distanceToEnemy = Vector3.Distance(transform.position, targetPos);
            if (distanceToEnemy < explosionRadius)
            {
                enemyToTrack.GetComponent<EnemyHandler>().DecreaseHealth(bulletDamage);
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        } else
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        }   
    }

    
    public void setDirection(Vector3 direction)
    {
        dir = direction;
    }


    private void OnTriggerEnter(Collider other) {
        if (enemyToTrack == null && other.CompareTag("Enemy"))
        {
            enemyToTrack = other.gameObject;
        }
    }
}
