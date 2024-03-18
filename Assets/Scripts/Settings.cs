using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Settings : MonoBehaviour
{

    [Header("General")]

    [SerializeField] GameObject settingsWindow;
    
    [SerializeField] GameObject openSettingsButton;
    [SerializeField] GameObject closeSettingsButton;
    [SerializeField] GameObject exitButton;

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider buttonsSizeSlider;

    [SerializeField] Toggle toggleMuteAudio;
    [SerializeField] Toggle toggleMuteHaptics;

    [SerializeField] Animator settingsAnimator;

    public float buttonsSize { get; private set; }

    [SerializeField] GameObject sideMovementButtons;
    [SerializeField] GameObject jumpButton;

    [SerializeField] TextMeshProUGUI fpsText;
    float fps;

    public float difficulty;

    private Animation toggleSettings_AnimationController;
    [SerializeField] private AnimationClip toggleSettings_VanishClip;
    [SerializeField] private AnimationClip toggleSettings_ResetClip;

    public Action onSettingsOpen;
    public Action onSettingsClose;

    public void Init()
    {
        toggleSettings_AnimationController = openSettingsButton.GetComponent<Animation>();
        toggleSettings_VanishClip.legacy = true;
        toggleSettings_ResetClip.legacy = true;
        Environment.gameStateController.onEnterGame += GameEnterCallback;
        onSettingsOpen += SettingsOpenCallback;

        buttonsSize = jumpButton.transform.localScale.x;

        if (Application.isMobilePlatform)
        {
            sideMovementButtons.SetActive(true);
            jumpButton.SetActive(true);
            openSettingsButton.SetActive(true);
        }
        else
        {
            /*sideMovementButtons.SetActive(false);
            jumpButton.SetActive(false);
            openSettingsButton.SetActive(false);*/

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Application.targetFrameRate = 80;

        LoadSavedSettings();
    }
    private void OnDestroy()
    {
        Environment.gameStateController.onEnterGame -= GameEnterCallback;
        onSettingsOpen -= SettingsOpenCallback;
    }

    private void Update()
    {
        fps = Mathf.RoundToInt(1f / Time.deltaTime);
        fpsText.text = $"FPS:{fps}";
    }

    public void OpenSettings()
    {
        // To avoid unexpected bugs, prohibit opening settings whilst death animation is running
        if (!Environment.playerController.isAlive) return;
        if (Environment.savingSystem.settingsOpened) return;

        Environment.gameStateController.StopGameSession();
        onSettingsOpen?.Invoke();

        Environment.gameStateController.gameState = GameStateController.State.Freezed;
        Environment.playerController.gameObject.SetActive(false);
        Environment.savingSystem.settingsOpened = true;
        //settingsWindow.SetActive(true);
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
        if (Environment.savingSystem.settingsOpened == false) return;
        
        onSettingsClose?.Invoke();

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
            //settingsWindow.SetActive(false);
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
    public void SetVolume()
    {
        Environment.savingSystem.volume = volumeSlider.value;
    }
    private void LoadSavedSettings()
    {
        buttonsSizeSlider.value = Environment.savingSystem.buttonSize;
        SetButtonsSize();

        volumeSlider.value = Environment.savingSystem.volume;

        toggleMuteAudio.isOn = Environment.savingSystem.audioMuted;
        toggleMuteHaptics.isOn = Environment.savingSystem.hapticsMuted;
    }

    public void OpenGithubPage()
    {
        Application.OpenURL("https://github.com/AnanasikDev/LollyJump");
    }
    public void ToggleHaptics()
    {
        Environment.savingSystem.hapticsMuted = toggleMuteHaptics.isOn;
    }
    public void ToggleAudio()
    {
        Environment.savingSystem.audioMuted = toggleMuteAudio.isOn;
        if (Environment.savingSystem.audioMuted)
        {
            volumeSlider.GetComponent<SliderEffect>().SetDisabled();
            volumeSlider.interactable = false;
        }
        else
        {
            volumeSlider.GetComponent<SliderEffect>().SetEnabled();
            volumeSlider.interactable = true;
        }
    }

    private void GameEnterCallback()
    {
        // Start animation of disappearing of toggleSettings
        toggleSettings_AnimationController.Play("SettingsButtonVanish");
    }
    private void SettingsOpenCallback()
    {
        toggleSettings_AnimationController.Play("SettingsButtonReset");
    }
}
