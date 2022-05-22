using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject healthUI;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;
    private bool gameIsPaused;


    private void Awake() {
        Time.timeScale = 1;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else 
            {
                healthUI.SetActive(false);
                pauseUI.SetActive(true);
                Time.timeScale = 0;
                gameIsPaused = true;
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        healthUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Resume()
    {
        healthUI.SetActive(true);
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeHealth(int curHealth)
    {
        healthText.text = "HP." + curHealth.ToString();
    }
}
