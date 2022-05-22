using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject finishUI;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player"))
        {
            Time.timeScale = 0;
            finishUI.SetActive(true);
        }
    }
}
