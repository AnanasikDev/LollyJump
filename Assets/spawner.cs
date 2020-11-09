using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform min;
    public Transform max;
    public move sc;
    public Text Score;
    public GameObject TextHolder;
    public Transform y;
    private void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        while (sc.alive)
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)],
                    new Vector2(Random.Range(min.position.x, max.position.x), 
                    Random.Range(y.position.y-0.3f, y.position.y+0.3f)), Quaternion.identity);
                enemy.GetComponent<collide>().text = Score;
                enemy.GetComponent<collide>().textHolder = TextHolder;
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
