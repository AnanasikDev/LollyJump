using UnityEngine;
using CandyCoded.HapticFeedback;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (!Application.isMobilePlatform)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SavingSystem.settingsOpened) Environment.settings.CloseSettings();
                else Environment.settings.OpenSettings();

                Debug.Log("Settings toggles to " + SavingSystem.settingsOpened);

                return;
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                Time.timeScale = 1 - Time.timeScale;
                if (Time.timeScale == 0)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }

            else if (Input.anyKey && GameStateController.gameState == GameStateController.State.Freezed)
            {
                if (!SavingSystem.settingsOpened)
                    Environment.gameStateController.EnterGame();
            }

            if (GameStateController.gameState == GameStateController.State.Playing)
            {
                Environment.playerController.velocity.x = Input.GetAxisRaw("Horizontal");
                Environment.playerController.velocity.y = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;
            }
        }
        else
        {
            if (Input.touchCount > 0 && GameStateController.gameState == GameStateController.State.Freezed && !SavingSystem.settingsOpened)
            {
                Environment.gameStateController.EnterGame();
            }
        }
    }

    public void ButtonHaptic()
    {
        HapticFeedback.LightFeedback();
        //Handheld.Vibrate();
    }
}
