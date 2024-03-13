using CandyCoded.HapticFeedback;
using UnityEngine;

public class HapticsController : MonoBehaviour
{
    public void Init()
    {
        Environment.gameStateController.onDied += VibrateLight;
    }

    private void OnDestroy()
    {
        Environment.gameStateController.onDied -= VibrateLight;
    }

    public void VibrateLight() => Vibrate(0);
    public void VibrateMedium() => Vibrate(1);
    public void VibrateHeavy() => Vibrate(2);

    public void Vibrate(int intensity)
    {
        if (Environment.savingSystem.hapticsMuted) return;

        switch (intensity)
        {
            case 0:
                HapticFeedback.LightFeedback();
                break;

            case 1:
                HapticFeedback.MediumFeedback();
                break;

            case 2:
                HapticFeedback.HeavyFeedback();
                break;
        }
    }
}
