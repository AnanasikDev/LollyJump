using UnityEngine;

public class IdleFlashEffect : MonoBehaviour
{
    private Animator animator;
    private static float timeToActivateSeconds = 5;
    private bool active = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Environment.inputController.timeSinceLastAction >= timeToActivateSeconds && !Environment.savingSystem.settingsOpened && Environment.gameStateController.gameState == GameStateController.State.Freezed)
        {
            if (!active)
                BeginEffect();
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
