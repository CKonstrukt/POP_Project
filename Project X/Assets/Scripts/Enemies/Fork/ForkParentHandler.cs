using UnityEngine;

public class ForkParentHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float turnSpeed;
    [SerializeField] private ForkHandler childForkHandler;

    [Header("Animations")]
    [SerializeField] private AnimForkButter parentAnimScript;
    private bool spawnAnimHasEnded;


    private void OnEnable() {
        spawnAnimHasEnded = false;
    }

    private void Update() {
        if (!spawnAnimHasEnded) return;

        if (childForkHandler.canFire)
        {
            if (transform.localRotation.y != 0)
            {
                // float step = transform.rotation.y * Time.deltaTime;
                // transform.Rotate(Vector3.up, step);
                transform.localRotation = Quaternion.Euler(0, 0 ,0);
            } else 
            {
                childForkHandler.loadProjectile();
            }
        } else if (childForkHandler.canRotate)
        {
            Rotation();
        }
    }

    private void Rotation()
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 dir = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    public void EndOfSpawnAnimation()
    {
        spawnAnimHasEnded = true;
        parentAnimScript.animatorButter.SetBool("spawn", false);
        parentAnimScript.animatorFork.SetBool("spawn", false);
    }
}
