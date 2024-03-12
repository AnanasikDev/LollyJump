﻿using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static State gameState = State.Freezed;

    private new Camera camera;
    private PostProcessProfile ppProfile;
    private ColorGrading ppColorGrading;
    private Grain ppGrain;

    public float timeSinceSessionStart { get { return Time.time - timeofstart; } }
    private float timeofstart;

    public void Init()
    {
        camera = Camera.main;
        ppProfile = camera.GetComponent<PostProcessVolume>().profile;
        ppColorGrading = ppProfile.GetSetting<ColorGrading>();
        ppGrain = ppProfile.GetSetting<Grain>();
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
        Environment.playerController.Respawn();
        Environment.playerController.Disactivate();
        Environment.enemySpawner.DeleteAllEntities();
    }
    public void EnterGame()
    {
        timeofstart = Time.time;
        gameState = State.Playing;

        Time.timeScale = 1;

        Environment.scoreController.SetScore(0);
        SavingSystem.lastScore = 0;

        Environment.scoreController.PlayRestartEffect();

        Environment.playerController.Activate();
    }
    public void ExitGame()
    {
        gameState = State.Freezed;

        Environment.playerController.Respawn();
    }
    public void ReloadScene()
    {
        SavingSystem.lastScore = Environment.scoreController.score;
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
                Time.timeScale = Mathf.Clamp01(Time.timeScale - 0.03f);
                ppColorGrading.saturation.value = Mathf.Clamp01(Mathf.Lerp(0, saturation, i/20f));
            }
            Time.timeScale = 0.25f;
            yield return new WaitForSecondsRealtime(0.5f);
            Environment.audioManager.SetVolume(1);

            ppGrain.active = false;
            ppColorGrading.active = false;

            ExitGame();
            ReloadScene();
            Environment.settings.Reload();
        }
        StartCoroutine(DeathShowcase());
    }
    public enum State
    {
        Playing,
        Freezed
    }
}
