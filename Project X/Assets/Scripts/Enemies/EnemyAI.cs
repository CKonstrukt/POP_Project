using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float turnSpeed;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] Transform player;

    [SerializeField] private float walkPointRange;
    private Vector3 walkPoint;
    bool walkPointSet;

    [HideInInspector] public bool canRotate, canMove;
    bool alreadyAttacked;

    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [HideInInspector] public bool spawnAnimHasEnded;


    private void OnEnable() 
    {   
        alreadyAttacked = false;
        animator.SetBool("spawn", true);
        spawnAnimHasEnded = false;
    }

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!spawnAnimHasEnded) return;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);    
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInSightRange && playerInAttackRange) Attack();

        if (AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            animator.SetBool("isAttacking", false);
            alreadyAttacked = false;
        }

        if (canRotate)
        {
            Rotation();
        }
    }

    private void Rotation()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 dir = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void Patrol()
    {
        if (!walkPointSet) 
        {
            SearchForWalkPoint();
        } else if(agent != null) {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchForWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        if (agent != null)
            agent.SetDestination(player.position);
    }

    private void Attack()
    {
        if (agent != null)
            agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("canAttack");
            animator.SetBool("isAttacking", true);

            alreadyAttacked = true;
        }
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
