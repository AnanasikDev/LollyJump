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

    public Vector2 buttonsSize;

    [SerializeField] GameObject sideMovementButtons;
    [SerializeField] GameObject jumpButton;

    [SerializeField] TextMeshProUGUI fpsText;
    float fps;

    public float difficulty;
    public static Settings instance { get; private set; }

    void Awake() => instance = this;
    private void Start()
    {
        buttonsSize = jumpButton.transform.localScale;

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
        /*if (!SavingSystem.settingsOpened)
        {
            SavingSystem.settingsOpened = true;
            //GameStateController.ExitGame();
            //GameStateController.ReloadScene();
            //return;
        }

        GameStateController.gameState = SavingSystem.state;
        settingsWindow.SetActive(true);

        */
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

        Debug.Log("Animator initialized: " + settingsAnimator.isInitialized);

        settingsAnimator.enabled = true;
        settingsAnimator.SetBool("Open", true);
        Debug.Log(settingsAnimator.GetBool("Open"));
    }
    public void CloseSettings()
    {
        Debug.Log("Settings closed");


        if (!Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        settingsAnimator.SetBool("Open", false);
        StartCoroutine(wait());

        IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            PlayerController.instance.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
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
        buttonsSize = new Vector2(buttonsSizeSlider.value, buttonsSizeSlider.value);
        jumpButton.transform.localScale = buttonsSize;
        sideMovementButtons.transform.localScale = buttonsSize;
    }         
    public void SetHardness()
    {
        difficulty = difficultySlider.value;
        //EnemySpawner.instance.SetTick(1 / hardness);
    }
    private void OnDestroy()
    {
        SavingSystem.buttonSize = buttonsSize;
        SavingSystem.difficulty = difficulty;
    }
    public void Reload()
    {
        buttonsSizeSlider.value = SavingSystem.buttonSize.x;
        SetButtonsSize();

        difficultySlider.value = SavingSystem.difficulty;
        SetHardness();
    }
}
