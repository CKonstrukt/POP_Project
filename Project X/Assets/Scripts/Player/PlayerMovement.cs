using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;

    public CharacterController controller;

    [SerializeField] private LayerMask layerMask;

    [Header("Movement")]
    [SerializeField] private GameObject vCam;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector3 dir;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;
    Vector3 velocity;

    [Header("Shooting")]
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireCooldown;
    private float nextTimeToFire = 0f;


    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Move();
        ApplyGravity();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            // Code for shooting projectiles.
            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
            {
                Vector3 targetPos = new Vector3(hit.point.x, projectileSpawnPoint.transform.position.y, hit.point.z);
                Vector3 dir = (targetPos - projectileSpawnPoint.transform.position).normalized;
                GameObject projectileInstance = Instantiate(projectile, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
                ProjectileHandler projectileHandler = projectileInstance.GetComponent<ProjectileHandler>();
                projectileHandler.setDirection(dir);

                nextTimeToFire = Time.time + fireCooldown;
            }

            Rotation(hit);
        }
    }

    void Rotation(RaycastHit raycastHit)
    {
        Vector3 targetPos = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
        Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    void Move()
    {
        float x, z;

        float positionOnTrack = vCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;
        if (positionOnTrack <= 1)
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
        } else {
            x = Input.GetAxisRaw("Vertical");
            z = -Input.GetAxisRaw("Horizontal");
        }
        

        dir = new Vector3(x, 0, z).normalized;

        if (dir.magnitude >= 0.1f)
        {
            controller.Move(dir * moveSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        } else 
        {
            velocity.y += gravity * Time.deltaTime; 
            controller.Move(velocity * Time.deltaTime);
        }
            
    }
}
