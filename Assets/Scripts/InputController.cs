using UnityEngine;

public class InputController : MonoBehaviour
{
    public float timeSinceLastAction { get { return Time.time - timeOfLastAction; } }
    private float timeOfLastAction;

    public void Init()
    {
        RecordLastAction();
    }

    private void RecordLastAction() => timeOfLastAction = Time.time;

    private void Update()
    {
        if (Environment.playerController.isAlive == false) return;

        if (!Application.isMobilePlatform)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Environment.savingSystem.settingsOpened)
                {
                    Environment.settings.CloseSettings();
                }
                else
                {
                    Environment.settings.OpenSettings();
                }
                RecordLastAction();

                return;
            }

            else if (Input.anyKey && Environment.gameStateController.gameState == GameStateController.State.Freezed)
            {
                if (!Environment.savingSystem.settingsOpened)
                {
                    Environment.gameStateController.EnterGame();
                    RecordLastAction();
                }
            }

            if (Environment.gameStateController.gameState == GameStateController.State.Playing)
            {
                Environment.playerController.velocity.x = Input.GetAxisRaw("Horizontal");
                Environment.playerController.velocity.y = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;
                RecordLastAction();
            }
        }
        else
        {
            if (Input.touchCount > 0 && Environment.gameStateController.gameState == GameStateController.State.Freezed && !Environment.savingSystem.settingsOpened)
            {
                if (Environment.savingSystem.settingsOpened) return;

                Environment.gameStateController.EnterGame();
                RecordLastAction();
            }
        }
    }
}
