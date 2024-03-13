using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public int lastScore;

    public float buttonSize = 0.25f;

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
