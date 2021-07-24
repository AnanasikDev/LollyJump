using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    int _score = 0;
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            text.text = _score.ToString();

            print(value);
        }
    }
    public TextMeshProUGUI text;

    public static ScoreController instance { get; private set; }
    void Awake() => instance = this;
    private void Start()
    {
        score = LastScoreHandler.lastScore;
    }
}