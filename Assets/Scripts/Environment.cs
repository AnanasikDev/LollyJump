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

    private void Start()
    {
        gameStateController = _gameStateController;
        gameStateController.Init();
        
        audioManager = _audioManager;
        audioManager.Init();

        settings = _settings;
        settings.Init();

        scoreController = _scoreController;
        scoreController.Init();

        inputController = _inputController;

        playerController = _playerController;
        playerController.Init();
        
        enemySpawner = _enemySpawner;
        enemySpawner.Init();
    }
}
