using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private float nextTimeToDash = 0f;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextTimeToDash)
        {
            nextTimeToDash = Time.time + dashCooldown;
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            playerMovement.controller.Move(playerMovement.dir * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }   
}
