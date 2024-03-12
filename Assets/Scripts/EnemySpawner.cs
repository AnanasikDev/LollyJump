using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemiesData;
    [HideInInspector] public List<GameObject> entities;
    private List<GameObject> warningsPool;

    [SerializeField] private EnemyController enemyControllerPrefab;
    public Transform leftBarrier;
    public Transform rightBarrier;
    public Transform heightOfSpawn;

    public float tick;

    public bool warn = false;
    public GameObject warning;
    public Transform warningsHandler;
    public float warningDurationSeconds = 1f;

    [SerializeField] Transform enemiesHandler;


    [Header("Difficulty regulations")]

    public AnimationCurve quantityFactorOverTime;
    public AnimationCurve frequencyFactorOverTime;

    public AnimationCurve quantityDistribution;
    public AnimationCurve sizeDistribution;
    private float quantityFactor { get { return quantityFactorOverTime.Evaluate(GameStateController.instance.timeSinceSessionStart); } }
    private float frequencyFactor { get { return frequencyFactorOverTime.Evaluate(GameStateController.instance.timeSinceSessionStart); } }

    public static EnemySpawner instance { get; private set; }
    private void Start()
    {
        instance = this;
        warningsPool = new List<GameObject>();
        StartCoroutine(Spawn());
    }
    private int CalculateQuantity()
    {
        return Mathf.RoundToInt(quantityDistribution.Evaluate(Random.value) * quantityFactor);
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
        return enemiesData[0];
    }
    public IEnumerator Spawn()
    {
        if (GameStateController.gameState == GameStateController.State.Playing)
        {
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

                    GameObject warningObject;

                    if (warningsPool.Any(w => !w.activeSelf))
                    {
                        // There is a cached object, fetch it
                        warningObject = warningsPool.First(w => !w.activeSelf);
                    }
                    else
                    {
                        warningObject = Instantiate(warning);
                        warningObject.transform.SetParent(warningsHandler);
                        warningsPool.Add(warningObject);
                    }
                    warningObject.SetActive(true);
                    warningObject.transform.localPosition = new Vector3(enemy.transform.position.x, 0, 0);
                    StartCoroutine(ExposeWarning(warning.gameObject, enemy.gameObject));
                }
                else
                {
                    enemy = Instantiate(enemyControllerPrefab,
                                        CalculatePosition(),
                                        Quaternion.identity,
                                        enemiesHandler);
                }
                entities.Add(enemy.gameObject);
                enemy.Init(data, CalculateSize());
            }
        }

        yield return new WaitForSeconds(tick * 1f/frequencyFactor);
        yield return Spawn();
    }
    public IEnumerator ExposeWarning(GameObject warning, GameObject ball)
    {
        yield return new WaitForSeconds(warningDurationSeconds);
        if (ball) ball.SetActive(true);
        if (warning) warning.SetActive(false);
    }

    public void DeleteAllEntities()
    {
        foreach (var entity in entities)
        {
            Destroy(entity.gameObject);
        }
        entities.Clear();
        foreach (var warning in warningsPool)
        {
            warning.SetActive(false);
        }
    }
    public void AddEntity(GameObject entity) => entities.Add(entity);
}
