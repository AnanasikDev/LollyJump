using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (!Application.isMobilePlatform)
        {
            if (Input.anyKey && GameStateController.gameState == GameStateController.State.Freezed)
            {
                GameStateController.EnterGame();
            }
            PlayerController.instance.velocity.x = Input.GetAxisRaw("Horizontal");
            PlayerController.instance.velocity.y = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;
        }
    }
}
