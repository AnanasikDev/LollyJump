using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateController : MonoBehaviour
{

    public static State gameState = State.Freezed;
    public static void EnterGame()
    {
        gameState = State.Playing;

        Time.timeScale = 1;

        ScoreController.instance.score = 0;

    }
    public static void ExitGame()
    {
        gameState = State.Freezed;

        LastScoreHandler.lastScore = ScoreController.instance.score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 0;

        PlayerController.instance.Respawn();
    }
    public enum State
    {
        Playing,
        Freezed
    }
}
