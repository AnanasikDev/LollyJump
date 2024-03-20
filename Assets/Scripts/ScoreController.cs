using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private Animation maxScoreAnimation;
    [SerializeField] private AnimationClip maxScoreVanishClip;
    [SerializeField] private AnimationClip maxScoreResetClip;

    public int score { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    public void Init()
    {
        SetScore(Environment.savingSystem.lastScore);
        maxScoreAnimation = maxScoreText.GetComponent<Animation>();
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

    public void SetScore(int n)
    {
        score = n;
        Environment.savingSystem.maxScore = (score > Environment.savingSystem.maxScore) ? score : Environment.savingSystem.maxScore;
        scoreText.text = score.ToString();
    }
    public void IncreaseScore(int n) => SetScore(score + n);
}