using TMPro;
using UnityEngine;

public class IdleFlashEffect : MonoBehaviour
{
    private Animator animator;
    private static float timeToActivateSeconds = 5;
    private static float timeToActivateEastereggSeconds = 73; // 73
    private static float timeToDisactivateEastereggSeconds = 95; // 95
    private bool active = false;

    [SerializeField] private TextMeshProUGUI text;

    private string baseEffect = "Tap anywhere\nto start";
    private string eastereggEffect = "Why don't you?";

    private void Start()
    {
        animator = GetComponent<Animator>();
        text.text = baseEffect;
    }

    private void Update()
    {
        if (Environment.inputController.timeSinceLastAction >= timeToActivateSeconds && !Environment.savingSystem.settingsOpened && Environment.gameStateController.gameState == GameStateController.State.Freezed)
        {
            if (!active)
                BeginEffect();
            else if (Environment.inputController.timeSinceLastAction >= timeToActivateEastereggSeconds &&
                     Environment.inputController.timeSinceLastAction < timeToDisactivateEastereggSeconds)
            {
                text.text = eastereggEffect;
            }
            else if (Environment.inputController.timeSinceLastAction >= timeToDisactivateEastereggSeconds)
            {
                text.text = baseEffect;
            }
        }
        else if (active)
        {
            StopEffect();
        }
    }
    public void BeginEffect()
    {
        active = true;
        animator.SetBool("Active", true);
    }
    public void StopEffect()
    {
        active = false;
        animator.SetBool("Active", false);
    }
}
