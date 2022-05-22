using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private UIHandler uiHandler;

    [SerializeField] private int maxPlayerHealth;
    private int currentPlayerHealth;
    private PlayerMovement playerMovement;

    private float triggerCheckFirst, triggerCheckSecond;
    private int triggerCount;

    void Start()
    {
        triggerCount = 0;
        triggerCheckFirst = Time.time;
        triggerCheckSecond = Time.time;
        currentPlayerHealth = maxPlayerHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (currentPlayerHealth <= 0)
        {
            uiHandler.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (triggerCount == 0 && triggerCheckSecond - triggerCheckFirst >= 0.25f && other.tag == "Enemy")
        {
            triggerCheckFirst = Time.time;
            EnemyHandler enemyHandler = other.GetComponent<EnemyHandler>();
            DecreaseHealth(enemyHandler.damageToPlayer);
        }
        triggerCount++;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy") {
            triggerCheckSecond = Time.time;
            triggerCount = 0;
        }
    }

    public void DecreaseHealth(int amount)
    {
        currentPlayerHealth -= amount;
        uiHandler.ChangeHealth(currentPlayerHealth);
    }
}
