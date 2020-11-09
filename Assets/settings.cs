using UnityEngine.UI;
using UnityEngine;

public class settings : MonoBehaviour
{
    public GameObject settingsWindow;
    public GameObject player;

    public Slider ButtonSizeSlider;
    public GameObject left_right;
    public Button jump;

    public GameObject score;
    public Slider TextSizeSlider;

    private Vector3 score_size;

    private Vector3 btn_size;
    private Vector3 leftright_size;

    public bool opened;
    private void Start()
    {
        btn_size = jump.transform.localScale;
        leftright_size = left_right.transform.localScale;

        score_size = score.transform.localScale;

        settingsWindow.SetActive(false);
    }
    public void OpenSettings()
    {
        settingsWindow.SetActive(true);
        opened = true;
        player.GetComponent<move>().MoveStop();
    }
    public void CloseSettings()
    {
        settingsWindow.SetActive(false);
        opened = false;
        player.GetComponent<move>().MoveStop();
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
        score.transform.localScale = score_size * value;
    }                                                    
}
