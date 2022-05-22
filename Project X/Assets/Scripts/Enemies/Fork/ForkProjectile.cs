using UnityEngine;

public class ForkProjectile : MonoBehaviour
{
    [Header("General")]
    [HideInInspector] public bool isParented = true;
    [HideInInspector] public Transform parent;
    private Transform player;

    [Header("Damage")]
    public int damageToPlayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Collision detection")]
    [SerializeField] private LayerMask physicsLayer;
    [SerializeField] private float distToGround;
    [SerializeField] private float distToPlayer;
    [SerializeField] private float playerHeightAdjustment;
    private Vector3 originalPlayerPos;
    private Vector3 direction;


    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (isParented)
        {
            transform.position = parent.position;

            originalPlayerPos = new Vector3(player.position.x, player.position.y + playerHeightAdjustment, player.position.z);
            direction = (originalPlayerPos - transform.position).normalized ;
        } else
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        
            // Custom collision, because the built one in doesnt work. It's something I did wrong, but cant find what.
            Vector3 curPlayerPos = new Vector3(player.position.x, player.position.y + playerHeightAdjustment, player.position.z);
            if (Physics.Raycast(transform.position, Vector3.down, distToGround, physicsLayer))
            {
                Destroy(gameObject);
            } else if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitForward, distToGround, physicsLayer))
            {
                if (hitForward.transform.CompareTag("Player"))
                {
                    hitForward.transform.GetComponent<HealthHandler>().DecreaseHealth(damageToPlayer);
                }
                Destroy(gameObject);
            } else if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hitLeft, distToGround, physicsLayer))
            {
                if (hitLeft.transform.CompareTag("Player"))
                {
                    hitLeft.transform.GetComponent<HealthHandler>().DecreaseHealth(damageToPlayer);
                }
                Destroy(gameObject);
            } else if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitBack, distToGround, physicsLayer))
            {
                if (hitBack.transform.CompareTag("Player"))
                {
                    hitBack.transform.GetComponent<HealthHandler>().DecreaseHealth(damageToPlayer);
                }
                Destroy(gameObject);
            } else if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitRight, distToGround, physicsLayer))
            {
                if (hitRight.transform.CompareTag("Player"))
                {
                    hitRight.transform.GetComponent<HealthHandler>().DecreaseHealth(damageToPlayer);
                }
                Destroy(gameObject);
            }
        }
    }
}
