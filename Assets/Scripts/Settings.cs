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
    public static Settings instance { get; private set; }

    void Awake() => instance = this;
    private void Start()
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SavingSystem.settingsOpened = !SavingSystem.settingsOpened;
            if (SavingSystem.settingsOpened) OpenSettings();
            else CloseSettings();
        }
        fps = Mathf.RoundToInt(1f / Time.deltaTime);
        fpsText.text = $"FPS:{fps}";
    }

    public void OpenSettings()
    {
        // To avoid unexpected bugs, prohibit opening settings whilst death animation is running
        if (!PlayerController.instance.isAlive) return;

        GameStateController.StopGameSession();

        GameStateController.gameState = GameStateController.State.Freezed;
        PlayerController.instance.gameObject.SetActive(false);
        SavingSystem.settingsOpened = true;
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
            PlayerController.instance.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.6f);
            SavingSystem.settingsOpened = false;
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
        SavingSystem.buttonSize = buttonsSize;
    }
    public void Reload()
    {
        buttonsSizeSlider.value = SavingSystem.buttonSize;
        SetButtonsSize();
    }
}
