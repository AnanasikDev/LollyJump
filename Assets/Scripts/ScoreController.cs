using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Animator maxScoreAnimator;

    public int score { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    public void Init()
    {
        SetScore(Environment.savingSystem.lastScore);
        Environment.gameStateController.onEnterGame += MaxScore_Vanish;
        //Environment.settings.onSettingsOpen += OpenSettingsCallback;
        maxScoreText.text = $"max: {Environment.savingSystem.maxScore}";
    }
    private void OnDestroy()
    {
        Environment.gameStateController.onEnterGame -= MaxScore_Vanish;
        //Environment.settings.onSettingsOpen -= OpenSettingsCallback;
    }

    private void MaxScore_Vanish()
    {
        maxScoreAnimator.SetTrigger("MaxScoreVanish");
    }
    private void OpenSettingsCallback()
    {
    }

    public void SetScore(int n)
    {
        score = n;
        Environment.savingSystem.maxScore = (score > Environment.savingSystem.maxScore) ? score : Environment.savingSystem.maxScore;
        scoreText.text = score.ToString();
    }
    public void IncreaseScore(int n) => SetScore(score + n);
}