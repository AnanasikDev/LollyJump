using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    [SerializeField] ParticleSystem[] AdditionEffectParticles;
    [SerializeField] ParticleSystem[] RestartEffectParticles;
    Animator anim;

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
                PlayAdditionEffect();
            else
                PlayRestartEffect();
        }
    }
    public TextMeshProUGUI text;

    public static ScoreController instance { get; private set; }
    void Awake() => instance = this;
    private void Start()
    {
        score = LastScoreHandler.lastScore;
        anim = text.GetComponent<Animator>();
    }
    void PlayAdditionEffect()
    {
        foreach (ParticleSystem particleSystem in AdditionEffectParticles)
        {
            ParticleSystem particles = Instantiate(particleSystem, transform);
            particles.Play();
            Destroy(particles, 3);
        }
        anim = text.GetComponent<Animator>();
        anim.SetTrigger("Add");
    }
    void PlayRestartEffect()
    {
        foreach (ParticleSystem particleSystem in RestartEffectParticles)
        {
            ParticleSystem particles = Instantiate(particleSystem, transform);
            particles.Play();
            Destroy(particles, 3);
        }
    }
}