using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    //[SerializeField] ParticleSystem[] AdditionEffectParticles;
    //[SerializeField] ParticleSystem[] RestartEffectParticles;
    private Animator scoreAnimator;

    private Animation maxScoreAnimation;
    [SerializeField] private AnimationClip maxScoreVanishClip;
    [SerializeField] private AnimationClip maxScoreResetClip;

    public int score { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    public void Init()
    {
        SetScore(Environment.savingSystem.lastScore);
        scoreAnimator = scoreText.GetComponent<Animator>();
        maxScoreAnimation = maxScoreText.GetComponent<Animation>();
        //maxScoreAnimation.GetClip("MaxScoreVanish").legacy = true;
        maxScoreVanishClip.legacy = true;
        maxScoreResetClip.legacy = true;
        Environment.gameStateController.onEnterGame += EnterGameCallback;
        Environment.settings.onSettingsOpen += OpenSettingsCallback;
        maxScoreText.text = $"max: {Environment.savingSystem.maxScore}";
    }
    private void OnDestroy()
    {
        Environment.gameStateController.onEnterGame -= EnterGameCallback;
        Environment.settings.onSettingsOpen -= OpenSettingsCallback;
    }

    private void EnterGameCallback()
    {
        maxScoreAnimation.Play("MaxScoreVanish");
    }
    private void OpenSettingsCallback()
    {
        maxScoreAnimation.Play("MaxScoreReset");
    }

    public void PlayAdditionEffect()
    {
        /*foreach (ParticleSystem particleSystem in AdditionEffectParticles)
        {
            ParticleSystem particles = Instantiate(particleSystem, transform);
            particles.Play();
            Destroy(particles, 3);
        }*/
        scoreAnimator = scoreText.GetComponent<Animator>();
        scoreAnimator.SetTrigger("Add");
    }
    public void PlayRestartEffect()
    {
        /*foreach (ParticleSystem particleSystem in RestartEffectParticles)
        {
            ParticleSystem particles = Instantiate(particleSystem, transform);
            particles.Play();
            Destroy(particles, 3);
        }*/
    }

    public void SetScore(int n)
    {
        score = n;
        Environment.savingSystem.maxScore = (score > Environment.savingSystem.maxScore) ? score : Environment.savingSystem.maxScore;
        scoreText.text = score.ToString();

        //if (score > 0) // Do not play on awake
        //    PlayAdditionEffect();
    }
    public void IncreaseScore(int n) => SetScore(score + n);
}