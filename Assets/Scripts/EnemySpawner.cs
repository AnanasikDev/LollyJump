using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemiesData;
    [SerializeField] private EnemyController enemyControllerPrefab;
    public Transform leftBarrier;
    public Transform rightBarrier;
    public Transform heightOfSpawn;

    public float tick;

    public bool warn = false;
    public GameObject warning;
    public Transform warningsHandler;
    public float warningDurationSeconds = 1f;

    public AnimationCurve quantityDistribution;
    public AnimationCurve sizeDistribution;
    public AnimationCurve tickDistribution;

    [SerializeField] Transform enemiesHandler;
    public static EnemySpawner instance { get; private set; }
    private void Start()
    {
        instance = this;
        StartCoroutine(Spawn());

        Settings.instance.Reload();
    }
    private int CalculateQuantity()
    {
        return Mathf.RoundToInt(quantityDistribution.Evaluate(Random.value));
    }
    private float CalculateSize()
    {
        return sizeDistribution.Evaluate(Random.value);
    }
    private Vector2 CalculatePosition()
    {
        return new Vector2
            (
                Random.Range(leftBarrier.position.x, rightBarrier.position.x),
                Random.Range(heightOfSpawn.position.y - 0.3f, heightOfSpawn.position.y + 0.3f)
            );
    }
    private EnemyData ChooseBallModel()
    {
        float v = Random.value;
        float min = 0;
        foreach (var model in enemiesData)
        {
            if (v > min && v < min + model.frequency)
            {
                return model;
            }
            min += model.frequency;
        }
        Debug.LogError("Unable to choose ball model. Default is chosen");
        return null;
    }
    public IEnumerator Spawn()
    {
        if (GameStateController.gameState == GameStateController.State.Playing)
        for (int i = 0; i < CalculateQuantity(); i++)
        {
            EnemyData data = ChooseBallModel();

            EnemyController enemy;
            if (warn)
            {
                enemy = Instantiate(enemyControllerPrefab,
                                    CalculatePosition(),
                                    Quaternion.identity,
                                    enemiesHandler);

                enemy.gameObject.SetActive(false);
                GameObject warningObject = Instantiate(warning);
                warningObject.transform.SetParent(warningsHandler);
                warningObject.transform.localPosition = new Vector3(enemy.transform.position.x, 0, 0);
                Destroy(warningObject, warningDurationSeconds);
                StartCoroutine(Show(enemy.gameObject));
            }
            else
            {
                enemy = Instantiate(enemyControllerPrefab,
                                    CalculatePosition(),
                                    Quaternion.identity,
                                    enemiesHandler);
            }
            enemy.Init(data, CalculateSize());
        }
        yield return new WaitForSeconds(tick);
        yield return Spawn();
    }
    public IEnumerator Show(GameObject ball)
    {
        yield return new WaitForSeconds(1);
        ball.SetActive(true);
    }
}
