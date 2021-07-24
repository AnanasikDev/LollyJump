using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingsWindow;

    public Slider ButtonSizeSlider;
    public GameObject left_right;
    public Button jump;

    public Slider TextSizeSlider;

    private float score_size;

    private Vector3 btn_size;
    private Vector3 leftright_size;

    public Slider HardnessSlider;

    public Toggle UseImpulseToggle;
    public Toggle ShowShadowsToggle;

    public bool opened;

    public static Settings instance { get; private set; }
    void Awake() => instance = this;
    private void Start()
    {
        btn_size = jump.transform.localScale;
        leftright_size = left_right.transform.localScale;

        score_size = ScoreController.instance.text.fontSize;

        settingsWindow.SetActive(false);
    }
    public void OpenSettings()
    {
        settingsWindow.SetActive(true);
        ScoreController.instance.text.gameObject.SetActive(false);
        opened = true;
        GameStateController.ExitGame();
    }
    public void CloseSettings()
    {
        settingsWindow.SetActive(false);
        ScoreController.instance.text.gameObject.SetActive(true);
        opened = false;
        GameStateController.ExitGame();
    }

    public void SetButtonsSize()
    {
        float value = ButtonSizeSlider.value;
        jump.transform.localScale = btn_size * value;
        left_right.transform.localScale = leftright_size * value;
    }
    public void SetTextSize()
    {                                                   
        float value = TextSizeSlider.value;             
        ScoreController.instance.text.fontSize = score_size * value;
    }              
    public void SetHardness()
    {
        EnemySpawner.instance.SetTick(1 / HardnessSlider.value);
        StopCoroutine(EnemySpawner.instance.Spawn());
        StartCoroutine(EnemySpawner.instance.Spawn());
    }
    public void SetMovementMode()
    {
        PlayerController.instance.MoveImpulse = UseImpulseToggle.isOn;
    }
    public void SetShadows()
    {
        EnemySpawner.instance.PreShow = ShowShadowsToggle.isOn;
    }
}
