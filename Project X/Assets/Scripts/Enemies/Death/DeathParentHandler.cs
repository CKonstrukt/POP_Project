using UnityEngine;

public class DeathParentHandler : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private Transform cam;


    private void Start() {
        cam = Camera.main.transform;
    }

    private void Update() {
        Vector3 targetDirection = cam.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotateSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
