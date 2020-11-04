using UnityEngine.UI;
using UnityEngine;

public class settings : MonoBehaviour
{
    public GameObject settingsWindow;

    public Slider ButtonSizeSlider;
    public GameObject left_right;
    public Button jump;
    private Vector3 btn_size;
    private Vector3 leftright_size;
    private void Start()
    {
        btn_size = jump.transform.localScale;
        leftright_size = left_right.transform.localScale;
        settingsWindow.SetActive(false);
    }
    public void OpenSettings()
    {
        settingsWindow.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsWindow.SetActive(false);
    }

    public void SetButtonsSize()
    {
        float value = ButtonSizeSlider.value;
        jump.transform.localScale = btn_size * value;
        left_right.transform.localScale = leftright_size * value;
    }
}
