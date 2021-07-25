using UnityEngine.UI;
using UnityEngine;
public class Settings : MonoBehaviour
{

    [Header("General")]

    [SerializeField] GameObject PCEditionSettingsWindow;
    [SerializeField] GameObject MobileEditionSettingsWindow;
    
    [SerializeField] GameObject openSettingsButton;

    [SerializeField] PlatformSettings[] platformSettings; // PC, Mobile
    [SerializeField] PlatformSettings currentPlatformSettings;

    public bool opened;
    public static Settings instance { get; private set; }

    [Header("Settings")]

    public Vector2 buttonsSize;

    [SerializeField] GameObject sideMovementButtons;
    [SerializeField] GameObject jumpButton;

    public float hardness;
    public bool moveImpulse;
    public bool showShadows;

    void Awake() => instance = this;
    private void Start()
    {
        buttonsSize = jumpButton.transform.localScale;

        if (Application.isMobilePlatform) MobileEditionSettingsWindow.SetActive(false);
        else PCEditionSettingsWindow.SetActive(false);

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
        }

        if (!Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (SavingSystem.settingsOpened)
        {
            OpenSettings();
        }

        if (Application.isMobilePlatform)
        {
            currentPlatformSettings = platformSettings[1];
        }
        else
        {
            currentPlatformSettings = platformSettings[0];
        }

    }
    public void OpenSettings()
    {
        if (!SavingSystem.settingsOpened)
        {
            SavingSystem.settingsOpened = true;
            GameStateController.ExitGame();
            GameStateController.ReloadScene();
            return;
        }

        GameStateController.gameState = SavingSystem.state;
        if (Application.isMobilePlatform) MobileEditionSettingsWindow.SetActive(true);
        else PCEditionSettingsWindow.SetActive(true);

        PlayerController.instance.gameObject.SetActive(false);
        opened = true;

        if (!Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void CloseSettings()
    {
        SavingSystem.settingsOpened = false;

        if (Application.isMobilePlatform) MobileEditionSettingsWindow.SetActive(false);
        else PCEditionSettingsWindow.SetActive(false);

        PlayerController.instance.gameObject.SetActive(true);
        opened = false;

        if (!Application.isMobilePlatform)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        buttonsSize = new Vector2(currentPlatformSettings.buttonsSizeSlider.value, currentPlatformSettings.buttonsSizeSlider.value);
        jumpButton.transform.localScale = buttonsSize;
        sideMovementButtons.transform.localScale = buttonsSize;
    }         
    public void SetHardness()
    {
        hardness = currentPlatformSettings.hardnessSlider.value;
        EnemySpawner.instance.SetTick(1 / hardness);
    }
    public void SetMovementMode()
    {
        moveImpulse = currentPlatformSettings.moveImpulseToggle.isOn;
        PlayerController.instance.MoveImpulse = moveImpulse;
    }
    public void SetShadows()
    {
        showShadows = currentPlatformSettings.showShadowsToggle.isOn;
        EnemySpawner.instance.PreShow = showShadows;
    }
    private void OnDestroy()
    {
        SavingSystem.buttonSize = buttonsSize;
        SavingSystem.hardness = hardness;
        SavingSystem.moveImpulse = moveImpulse;
        SavingSystem.showShadows = showShadows;
    }
    public void Reload()
    {
        if (currentPlatformSettings.platformName == "Mobile")
        {
            currentPlatformSettings.buttonsSizeSlider.value = SavingSystem.buttonSize.x;
            SetButtonsSize();
        }

        currentPlatformSettings.hardnessSlider.value = SavingSystem.hardness;
        SetHardness();

        currentPlatformSettings.moveImpulseToggle.isOn = SavingSystem.moveImpulse;
        SetMovementMode();

        currentPlatformSettings.showShadowsToggle.isOn = SavingSystem.showShadows;
        SetShadows();
    }
}

[System.Serializable]
public class PlatformSettings
{
    public string platformName;
    public Slider buttonsSizeSlider;
    public Slider hardnessSlider;
    public Toggle moveImpulseToggle;
    public Toggle showShadowsToggle;
}