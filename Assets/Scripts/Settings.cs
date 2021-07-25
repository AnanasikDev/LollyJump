using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{

    [Header("General")]

    public GameObject settingsWindow;
    public bool opened;
    public static Settings instance { get; private set; }

    [Header("Settings")]

    [SerializeField] Slider buttonsSizeSlider;
    public Vector2 buttonsSize;

    [SerializeField] GameObject sideMovementButtons;
    [SerializeField] GameObject jumpButton;
    
    [SerializeField] Slider hardnessSlider;
    public float hardness;

    [SerializeField] Toggle moveImpulseToggle;
    public bool moveImpulse;

    [SerializeField] Toggle showShadowsToggle;
    public bool showShadows;

    void Awake() => instance = this;
    private void Start()
    {
        buttonsSize = jumpButton.transform.localScale;

        settingsWindow.SetActive(false);

        if (SavingSystem.opened)
        {
            OpenSettings();
        }
    }
    public void OpenSettings()
    {
        if (!SavingSystem.opened)
        {
            SavingSystem.opened = true;
            GameStateController.ReloadScene();
            GameStateController.ExitGame();
        }

        settingsWindow.SetActive(true);
        PlayerController.instance.gameObject.SetActive(false);
        opened = true;
    }
    public void CloseSettings()
    {
        SavingSystem.opened = false;

        settingsWindow.SetActive(false);
        PlayerController.instance.gameObject.SetActive(true);
        opened = false;
        //GameStateController.ExitGame();
        //GameStateController.ReloadScene();
    }

    public void SetButtonsSize()
    {
        buttonsSize = new Vector2(buttonsSizeSlider.value, buttonsSizeSlider.value);
        jumpButton.transform.localScale = buttonsSize;
        sideMovementButtons.transform.localScale = buttonsSize;
    }         
    public void SetHardness()
    {
        hardness = hardnessSlider.value;
        EnemySpawner.instance.SetTick(1 / hardness);
        /*StopCoroutine(EnemySpawner.instance.Spawn());
        StartCoroutine(EnemySpawner.instance.Spawn());*/
    }
    public void SetMovementMode()
    {
        moveImpulse = moveImpulseToggle.isOn;
        PlayerController.instance.MoveImpulse = moveImpulse;
    }
    public void SetShadows()
    {
        showShadows = showShadowsToggle.isOn;
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
        buttonsSizeSlider.value = SavingSystem.buttonSize.x;
        SetButtonsSize();

        hardnessSlider.value = SavingSystem.hardness;
        SetHardness();

        moveImpulseToggle.isOn = SavingSystem.moveImpulse;
        SetMovementMode();

        showShadowsToggle.isOn = SavingSystem.showShadows;
        SetShadows();
    }
}
