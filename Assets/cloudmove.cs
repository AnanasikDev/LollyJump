using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudmove : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        Destroy(gameObject, (2 - speed) * 9);
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed);
    }
}
