using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public EnemyController[] enemies;
    public Transform min;
    public Transform max;
    public Transform y;

    public float time;
    WaitForSeconds tick;

    public bool PreShow = false;
    public GameObject BallShadow;
    public Transform ShadowsHandler;

    [SerializeField] Transform enemiesHandler;
    public static EnemySpawner instance { get; private set; }
    short GetRandomAmount()
    {
        short[] ams = new short[14] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 4 };
        return ams[Random.Range(0, 14)];
    }
    public void SetTick(float tick)
    {
        time = tick;
        this.tick = new WaitForSeconds(tick);
    }
    public void Start()
    {
        instance = this;
        tick = new WaitForSeconds(time);
        StartCoroutine(Spawn());

        Settings.instance.Reload();
    }
    public IEnumerator Spawn()
    {
        if (GameStateController.gameState == GameStateController.State.Playing)
        for (int i = 0; i < GetRandomAmount(); i++)
        {
            EnemyController enemy;
            if (PreShow)
            {
                enemy = Instantiate(
                                    enemies[Random.Range(0, enemies.Length)],
                                    new Vector2(Random.Range(min.position.x, max.position.x),
                                    Random.Range(y.position.y - 0.3f, y.position.y + 0.3f)), Quaternion.identity,
                                    enemiesHandler);
                enemy.gameObject.SetActive(false);
                GameObject shadow = Instantiate(BallShadow, new Vector3(enemy.transform.position.x, 5.1f), Quaternion.identity, ShadowsHandler);
                Destroy(shadow, 1);
                StartCoroutine(Show(enemy.gameObject));
            }
            else
            {
                enemy = Instantiate(
                                    enemies[Random.Range(0, enemies.Length)],
                                    new Vector2(Random.Range(min.position.x, max.position.x),
                                    Random.Range(y.position.y - 0.3f, y.position.y + 0.3f)), Quaternion.identity,
                                    enemiesHandler);
            }
        }
        yield return tick;
        yield return Spawn();
    }
    public IEnumerator Show(GameObject ball)
    {
        yield return new WaitForSeconds(1);
        ball.SetActive(true);
    }
}
