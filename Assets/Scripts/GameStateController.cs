using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateController : MonoBehaviour
{
    public static State gameState = State.Freezed;
    private void Start()
    {
        //Time.timeScale = 0;
        gameState = State.Freezed;
    }

    /// <summary>
    /// Restarts everything to the original state:
    /// respawns player's ball, deletes all particles,
    /// effects, and enemies, nullifies score etc.
    /// </summary>
    public static void StopGameSession()
    {
        gameState = State.Freezed;
        PlayerController.instance.Respawn();
        PlayerController.instance.Disactivate();
        EnemySpawner.instance.DeleteAllEntities();
    }
    public static void EnterGame()
    {
        Debug.Log("Entering game");
        gameState = State.Playing;

        Time.timeScale = 1;

        ScoreController.instance.SetScore(0);
        SavingSystem.lastScore = 0;

        ScoreController.instance.PlayRestartEffect();

        PlayerController.instance.Activate();
    }
    public static void ExitGame()
    {
        gameState = State.Freezed;

        //Time.timeScale = 0;

        PlayerController.instance.Respawn();
    }
    public static void ReloadScene()
    {
        SavingSystem.lastScore = ScoreController.instance.score;
        SavingSystem.state = gameState;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public enum State
    {
        Playing,
        Freezed
    }
}
