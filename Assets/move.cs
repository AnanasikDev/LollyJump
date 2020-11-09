using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 origin;

    public GameObject spawner;
    public GameObject score;
    public float x;
    public float y;
    public float speed;
    public float jumpspeed;
    public bool j;
    public bool alive = true;
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Respawn();
    } 
    void FixedUpdate()
    {
        if (!alive) { Respawn(); return; }
        if (alive)
        {
            rb.AddForce(Vector2.right * x * speed);
            //Debug.Log(x);
            if (y != 0)
            {
                rb.AddForce(Vector2.up * y * jumpspeed);
                y = 0;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        alive = other.gameObject.tag != "Ball" && other.gameObject.tag != "Floor";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        alive = other.gameObject.tag != "Ball" && other.gameObject.tag != "Floor";
    }
    public void Respawn()
    {
        MoveStop();
        transform.position = origin;
    }
    private void MoveFreeze()
    {
        rb.Sleep();
        spawner.GetComponent<spawner>().StopCoroutine("Spawn");
    }
    private void MoveUnFreeze()
    {
        rb.WakeUp();
        spawner.GetComponent<spawner>().StartCoroutine("Spawn");
    }
    public void MoveStop()
    {
        alive = false;
        MoveFreeze();

        int lastScore = score.GetComponent<Score>().score;
        //score.GetComponent<Score>().score = 0;
        score.GetComponent<Score>().Write(lastScore.ToString());
    }
    public void MoveStart()
    {
        alive = true;
        MoveUnFreeze();

        var sc = score.GetComponent<Score>();
        sc.score = 0;
        //score.GetComponent<Score>().score = 0;
        sc.Write(0.ToString());
    }
    public static class Cache
    {
        public static void Write(string path, int n, string end = "\n")
        {
            print("written");
            var writer = new StreamWriter(path, true);
            writer.Write(n.ToString() + end);
            writer.Close();
        }
        public static string GetLast(string path)
        {
            string[] text = File.ReadAllLines(path);
            return text[text.Length - 1].Remove('\n');
        }
    }
}