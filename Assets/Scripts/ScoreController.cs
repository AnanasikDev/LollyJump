using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    [SerializeField] ParticleSystem addParticles1;
    [SerializeField] ParticleSystem addParticles2;

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

            if (_score > 0) // Do not play on awake 
                AddEffect();
        }
    }
    public TextMeshProUGUI text;

    public static ScoreController instance { get; private set; }
    void Awake() => instance = this;
    private void Start()
    {
        score = LastScoreHandler.lastScore;
    }
    void AddEffect()
    {
        addParticles1.Play();
        addParticles2.Play();
    }
}