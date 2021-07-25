using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (!Application.isMobilePlatform)
        {
            if (Input.anyKey && GameStateController.gameState == GameStateController.State.Freezed)
            {
                if (!Settings.instance.opened)
                    GameStateController.EnterGame();
            }
            PlayerController.instance.velocity.x = Input.GetAxisRaw("Horizontal");
            PlayerController.instance.velocity.y = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Settings.instance.opened) Settings.instance.CloseSettings();
                else Settings.instance.OpenSettings();
            }
        }
        else
        {
            if (Input.touchCount > 0 && GameStateController.gameState == GameStateController.State.Freezed)
            {
                GameStateController.EnterGame();
            }
        }
    }
}
