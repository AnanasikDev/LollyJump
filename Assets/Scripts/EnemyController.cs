using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;

    [Tooltip("Number of lives (divisions) left")] public int livesLeft;
    [Tooltip("Number of balls to be spawned when hit the ground")] public int childrenNumber;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem collisionParticles;

    EnemyData settings;

    public void Init(EnemyData settings, float size, int livesLeft=-100, int childrenN=-1)
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.settings = settings;
        if (childrenN == -1) this.childrenNumber = settings.inheritance;
        else this.childrenNumber = childrenN;

        spriteRenderer.color = settings.color;

        if (livesLeft == -100) this.livesLeft = settings.maxLives;
        else this.livesLeft = livesLeft;

        transform.localScale = new Vector3(size, size, 1);
        spriteRenderer.color = settings.color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball") && other.gameObject.layer != 10)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                ScoreController.instance.IncreaseScore(1);
            }
            Die();
        }
    }
    void PlayCollisionParticles()
    {
        collisionParticles.transform.parent = null;
        collisionParticles.Play();
        Destroy(collisionParticles, 4);
    }
    void PlayDeathParticles()
    {
        deathParticles.transform.parent = null;
        //deathParticles.transform.localEulerAngles = new Vector3(90, 0, 0);
        deathParticles.Play();
        Destroy(deathParticles, 4);
    }
    private void Die()
    {
        if (livesLeft > 0)
        {
            for (int c = 0; c < childrenNumber; c++)
            {
                GameObject child = Instantiate(gameObject, transform.position + new Vector3((Random.value-0.5f)*2, Random.value), Quaternion.identity);
                var enemyController = child.GetComponent<EnemyController>();
                enemyController.Init(settings, transform.localScale.x / 1.5f, livesLeft - 1, childrenNumber - 1);
                Vector3 force = new Vector3(Random.Range(-settings.unpredictability, settings.unpredictability), 1) * settings.bounciness;
                enemyController.rigidbody2d.AddForce(force);
            }
            transform.localScale = new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.5f);
        }
        PlayDeathParticles();
        Destroy(gameObject);
        Destroy(this);
    }
}
