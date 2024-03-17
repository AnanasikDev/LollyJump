using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public int lastScore;
    public int maxScore;

    public float buttonSize = 0.25f;
    public float volume = 0.8f;
    public bool hapticsMuted = false;
    public bool audioMuted = false;

    public bool settingsOpened = false;

    public GameStateController.State state = GameStateController.State.Freezed;

    private void Awake()
    {
        if (Environment.savingSystem != null)
            Destroy(gameObject);
        else
            Environment.savingSystem = this;
        
        DontDestroyOnLoad(this.gameObject);
    }
}
