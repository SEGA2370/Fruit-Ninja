using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button restartBtn;  
    [SerializeField] private Button quitBtn;

    [Header("Volume Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool isPaused = false;

    private void Start()
    {
        // pause toggle
        pauseBtn.onClick.AddListener(TogglePause);

        // panel starts hidden
        pausePanel.SetActive(false);

        // button listeners
        continueBtn.onClick.AddListener(ContinueGame);
        restartBtn.onClick.AddListener(RestartGame);
        quitBtn.onClick.AddListener(QuitGame);

        // Hook sliders to AudioManager
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSfxVolume);

        // Optional: initialize sliders from PlayerPrefs
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVol", 1f);
    }

    private void Update()
    {
        // on Android this will also catch the Back button
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
            PlayerPrefs.SetFloat("SfxVol", sfxSlider.value);
        }
    }

    private void ContinueGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        // In editor, stop play-mode; in build, quit application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
