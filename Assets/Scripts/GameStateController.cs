using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
public class GameStateController : MonoBehaviour
{
    public static State gameState = State.Freezed;

    public static GameStateController instance;
    private new Camera camera;
    private PostProcessProfile ppProfile;
    private ColorGrading ppColorGrading;
    private Grain ppGrain;
    private void Start()
    {
        camera = Camera.main;
        ppProfile = camera.GetComponent<PostProcessVolume>().profile;
        ppColorGrading = ppProfile.GetSetting<ColorGrading>();
        ppGrain = ppProfile.GetSetting<Grain>();
        instance = this;
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

    public void Die()
    {
        IEnumerator DeathShowcase()
        {
            ppGrain.active = true;
            ppColorGrading.active = true;
            float saturation = ppColorGrading.saturation;
            Time.timeScale = 1;
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForFixedUpdate();
                //Time.timeScale = Mathf.Clamp01(Time.timeScale / 1.05f);
                Time.timeScale = Mathf.Clamp01(Time.timeScale - 0.03f);
                ppColorGrading.saturation.value = Mathf.Clamp01(Mathf.Lerp(0, saturation, i/20f));
            }
            Time.timeScale = 0.25f;
            yield return new WaitForSecondsRealtime(0.5f);

            ppGrain.active = false;
            ppColorGrading.active = false;

            GameStateController.ExitGame();
            GameStateController.ReloadScene();
            Settings.instance.Reload();
        }
        StartCoroutine(DeathShowcase());
    }
    public enum State
    {
        Playing,
        Freezed
    }
}
