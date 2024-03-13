using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    public static EnemySpawner enemySpawner;

    [SerializeField] private ScoreController _scoreController;
    public static ScoreController scoreController;

    [SerializeField] private InputController _inputController;
    public static InputController inputController;

    [SerializeField] private PlayerController _playerController;
    public static PlayerController playerController;

    [SerializeField] private Settings _settings;
    public static Settings settings;

    [SerializeField] private AudioManager _audioManager;
    public static AudioManager audioManager;

    [SerializeField] private GameStateController _gameStateController;
    public static GameStateController gameStateController;

    [SerializeField] private HapticsController _hapticsController;
    public static HapticsController hapticsController;

    public static SavingSystem savingSystem;

    public bool init { get; private set; }

    private void Start()
    {
        if (!init) Init();
    }
    public void Init()
    {
        init = true;

        gameStateController = _gameStateController;
        gameStateController.Init();
        
        audioManager = _audioManager;
        audioManager.Init();

        settings = _settings;
        settings.Init();

        scoreController = _scoreController;
        scoreController.Init();

        inputController = _inputController;
        hapticsController = _hapticsController;
        hapticsController.Init();

        playerController = _playerController;
        playerController.Init();
        
        enemySpawner = _enemySpawner;
        enemySpawner.Init();
    }
}
