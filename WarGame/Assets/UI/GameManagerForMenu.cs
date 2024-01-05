using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManagerForMenu : MonoBehaviour
{
    public GameObject deathUI;
    public Button restartButton;
    public Button quitButtonOnDeath;

    public GameObject bag;
    public GameObject pauseMenu; 
    public Button resumeButton; // 
    public Button quitButton; // 

    private bool isPaused = false;


    void Start()
    {
        pauseMenu.SetActive(false);
        deathUI.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(restartGame);
        quitButtonOnDeath.onClick.AddListener(QuitGame);

    }

    void Update()
    {
        // ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        // open bag
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isPaused)
            {
                openBag();
            }
            else
            {
                closeBag();
            }
        }
        if (ThirdPersonController.death ==true) {
            openDeathUI();
        }
    }
    void openDeathUI()
    {
        isPaused = true;
        deathUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void restartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        string sceneName = SceneManager.GetActiveScene().name;// get scene name
        SceneManager.LoadScene(sceneName);
        
        Time.timeScale = 1;
        deathUI.SetActive(false);
        ThirdPersonController.death = false;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void openBag()
    {
        isPaused = true;
        bag.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void closeBag()
    {
        isPaused = false;
        bag.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void QuitGame()
    {
        // 这里你可以添加任何退出游戏前的逻辑
        SceneManager.LoadScene("MainMenu"); // 加载主菜单场景
    }
}