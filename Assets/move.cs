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
        transform.position = origin; //new Vector3(-0.2f, 1, 0);
                                     //transform.Translate(Vector3.up * 10);
    }
    public void MoveStop()
    {
        rb.Sleep();
        alive = false;
        spawner.GetComponent<spawner>().StopCoroutine("Spawn");
        int lastScore = score.GetComponent<Score>().score;
        //score.GetComponent<Score>().score = 0;
        score.GetComponent<Score>().Write(lastScore.ToString());
    }
    public void MoveStart()
    {
        rb.WakeUp();
        alive = true;
        spawner.GetComponent<spawner>().StartCoroutine("Spawn");
        score.GetComponent<Score>().score = 0;
        //score.GetComponent<Score>().score = 0;
        score.GetComponent<Score>().Write(0.ToString());
    }
}

/*public static class Cache
{
    public static void Write(string path, object obj, string end="\n")
    {
        var writer = new StreamWriter(path, true);
        writer.Write(obj.ToString() + end);
        writer.Close();
    }
    public static string GetLast(string path)
    {
        var reader = new StreamReader(path);
        int len = path.Split('\n').Length;
        string[] text = File.ReadAllLines(path);
        return text[len - 1];
    }
}*/