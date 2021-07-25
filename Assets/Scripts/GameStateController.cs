using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateController : MonoBehaviour
{
    public static State gameState = State.Freezed;
    private void Start()
    {
        Time.timeScale = 0;
        gameState = State.Freezed;
    }
    public static void EnterGame()
    {
        gameState = State.Playing;

        Time.timeScale = 1;

        ScoreController.instance.score = 0;
        SavingSystem.lastScore = 0;

        ScoreController.instance.PlayRestartEffect();
    }
    public static void ExitGame()
    {
        gameState = State.Freezed;

        Time.timeScale = 0;

        PlayerController.instance.Respawn();
    }
    public static void ReloadScene()
    {
        SavingSystem.lastScore = ScoreController.instance.score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public enum State
    {
        Playing,
        Freezed
    }
}
