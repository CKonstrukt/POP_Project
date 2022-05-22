    using UnityEngine;

public class ForkHandler : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectile;
    private GameObject curProjectile;

    [SerializeField] Animator animator;
    [HideInInspector] public bool canFire = true;
    [HideInInspector] public bool canRotate = true;

    [Header("Sound")]
    [SerializeField] private AudioManager audioManager;


    public void loadProjectile()
    {
        animator.SetBool("loadProjectile", true);
        canFire = false;
        canRotate = false;
    }

    public void InverseCanFire()
    {
        canFire = !canFire;
    }

    public void InverseCanRotate()
    {
        canRotate = !canRotate;
    }

    public void DestroyActiveProjectile()
    {
        if (curProjectile != null && curProjectile.GetComponent<ForkProjectile>().isParented)
        {
            Destroy(curProjectile);
        }
    }

    public void SpawnProjectile()
    {
        curProjectile = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.Euler(0, 0, 0));
        curProjectile.GetComponent<ForkProjectile>().parent = projectileSpawnPoint;
    }

    public void ReleaseProjectile()
    {
        curProjectile.GetComponent<ForkProjectile>().isParented = false;
    }

    public void PlayWhooshSound()
    {
        audioManager.Play("ForkWhoosh");
    }
}
