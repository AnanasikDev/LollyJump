using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class Settings : MonoBehaviour
{

    [Header("General")]

    [SerializeField] GameObject settingsWindow;
    
    [SerializeField] GameObject openSettingsButton;
    [SerializeField] GameObject closeSettingsButton;
    [SerializeField] GameObject exitButton;

    [SerializeField] Slider difficultySlider;
    [SerializeField] Slider buttonsSizeSlider;

    [SerializeField] Animator settingsAnimator;

    public float buttonsSize;

    [SerializeField] GameObject sideMovementButtons;
    [SerializeField] GameObject jumpButton;

    [SerializeField] TextMeshProUGUI fpsText;
    float fps;

    public float difficulty;

    public void Init()
    {
        buttonsSize = jumpButton.transform.localScale.x;

        if (Application.isMobilePlatform)
        {
            sideMovementButtons.SetActive(true);
            jumpButton.SetActive(true);
            openSettingsButton.SetActive(true);
        }
        else
        {
            sideMovementButtons.SetActive(false);
            jumpButton.SetActive(false);
            openSettingsButton.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Application.targetFrameRate = 80;

        LoadSavedSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Environment.savingSystem.settingsOpened = !Environment.savingSystem.settingsOpened;
            if (Environment.savingSystem.settingsOpened) OpenSettings();
            else CloseSettings();
        }
        fps = Mathf.RoundToInt(1f / Time.deltaTime);
        fpsText.text = $"FPS:{fps}";
    }

    public void OpenSettings()
    {
        // To avoid unexpected bugs, prohibit opening settings whilst death animation is running
        if (!Environment.playerController.isAlive) return;

        GameStateController.StopGameSession();

        GameStateController.gameState = GameStateController.State.Freezed;
        Environment.playerController.gameObject.SetActive(false);
        Environment.savingSystem.settingsOpened = true;
        settingsWindow.SetActive(true);
        Time.timeScale = 1;

        if (!Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        settingsAnimator.enabled = true;
        settingsAnimator.SetBool("Open", true);
    }
    public void CloseSettings()
    {
        if (!Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        settingsAnimator.SetBool("Open", false);
        StartCoroutine(wait());

        // safe animation closure
        IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(0.4f);
            Environment.playerController.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.6f);
            Environment.savingSystem.settingsOpened = false;
            settingsWindow.SetActive(false);
        }
    }
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void SetButtonsSize()
    {
        buttonsSize = buttonsSizeSlider.value;
        jumpButton.transform.localScale = new Vector3(buttonsSize, buttonsSize, 1);
        sideMovementButtons.transform.localScale = new Vector3(buttonsSize, buttonsSize, 1);
        Environment.savingSystem.buttonSize = buttonsSize;
    }
    private void LoadSavedSettings()
    {
        buttonsSizeSlider.value = Environment.savingSystem.buttonSize;
        SetButtonsSize();
    }
}
