using UnityEngine;
using UnityEngine.UI;

public class collide : MonoBehaviour
{
    private Rigidbody2D rb;
    public Text text;
    public GameObject textHolder;
    public Score score;
    public int Hp;
    public GameObject SelfCopy;
    public bool spawnedByMySelf = false;
    private move sc;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //transform.localScale /= 4f;
        sc = GameObject.Find("Player").GetComponent<move>();
        
        if (transform.localScale.x > 0.6f / 4f)
        Hp = Random.Range(2, 3);

        score = textHolder.GetComponent<Score>();
        text = textHolder.GetComponent<Text>();

        if (!spawnedByMySelf)
        {
            transform.localScale *= Hp;
            //rb.mass *= v;
        }
    }
    private void Update()
    {
        if (!sc.alive) Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ball")
        {
            Hp--;
            if (Hp <= 0)
            {
                Destroy(gameObject);
                return;
            }
            transform.localScale /= 1.5f;
            GameObject copy = Instantiate(SelfCopy,
                transform.position + new Vector3(Random.Range(-2, 2), 1, 0), transform.rotation);
            copy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(6, 11));
            copy.GetComponent<collide>().spawnedByMySelf = true;
            //rb.AddTorque(Random.Range(-60, 60) * Random.Range(0.8f, 3f));
            rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(0, 1)) * 250);
            if (other.gameObject.tag == "Floor")
            {
                score.score++;
            }
        }
    }
}
