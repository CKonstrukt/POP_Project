using UnityEngine;

public class AnimKnife : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyAI scriptParentAI;
    private EnemyAI enemyAI;

    [Header("Sound")]
    [SerializeField] private AudioManager audioManager;


    private void Start() {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    public void SetCanRotate()
    {
        enemyAI.canRotate = !enemyAI.canRotate;
    }
    
    public void SetCanMove()
    {
        enemyAI.canMove = !enemyAI.canMove;
    }

    public void EndOfSpawnAnimation()
    {
        animator.SetBool("spawn", false);
        scriptParentAI.spawnAnimHasEnded = true;
    }

    public void PlaySlashSound()
    {
        audioManager.Play("KnifeSlash");
    }
}
