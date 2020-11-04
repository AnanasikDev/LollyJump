using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject[] clouds;
    public Transform min;
    public Transform max;

    public Transform xmin;
    public Transform xmax;
    private Transform[] xs;
    private void Start()
    {
        xs = new Transform[2] { xmin, xmax };
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        for (; ; )
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                var tr = Random.Range(0, 2);
                print("tr = " + tr);
                GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length - 1)],
                    new Vector2(xs[tr].position.x, Random.Range(min.position.y, max.position.y)), Quaternion.identity);
                float x = Random.Range(0.6f, 1.4f);
                float speed = Random.Range(0.007f, 0.01f);
                if (tr == 0)
                {
                    speed *= -1;
                }
                cloud.GetComponent<cloudmove>().speed = speed;
                cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * x,
                    cloud.transform.localScale.x * (x - 0.3f), cloud.transform.localScale.z);
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 4));
        }
    }
}
