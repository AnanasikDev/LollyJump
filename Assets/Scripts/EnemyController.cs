using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int Hp;
    public bool isClone = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (transform.localScale.x > 0.6f / 4f)
            Hp = Random.Range(1, 3);

        if (!isClone)
        {
            transform.localScale *= Hp;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball") && other.gameObject.layer != 10)
        {
            Hp--;
            if (Hp <= 0)
            {
                EnemySpawner.instance.spawnedEnemies.Remove(this);
                Destroy(gameObject);
                return;
            }
            transform.localScale /= 1.5f;
            GameObject copy = Instantiate(gameObject,
                transform.position + new Vector3(Random.Range(-1, 1), 0.5f, 0), transform.rotation);
            copy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(5, 7));
            copy.GetComponent<EnemyController>().isClone = true;
            
            rb.AddForce(new Vector2(Random.Range(-1, 1) * 2, Random.Range(0, 1)) * 50);

            if (other.gameObject.CompareTag("Floor"))
            {
                ScoreController.instance.score++;
            }
        }
    }

}
