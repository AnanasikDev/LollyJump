using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text text;
    void Update()
    {
        text.text = score.ToString();
    }
    public void Write(string s)
    {
        text.text = s;
    }
}