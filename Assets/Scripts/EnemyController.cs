using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;

    [Tooltip("Number of lives (divisions) left")] public int livesLeft;
    [Tooltip("Number of balls to be spawned when hit the ground")] public int childrenNumber;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem collisionParticles;

    EnemySettings settings;

    public void Init(EnemySettings settings, int size, int childrenN)
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.settings = settings;
        this.childrenNumber = childrenN;

        spriteRenderer.color = settings.color;

        livesLeft = size;
        transform.localScale = new Vector3(size, size, 1);
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
                enemyController.Init(settings, livesLeft - 1, childrenNumber - 1);
                Vector3 force = new Vector3(Random.Range(-settings.unpredictability, settings.unpredictability), 1) * settings.bounciness;
                enemyController.rigidbody2d.AddForce(force);
            }
        }
        PlayDeathParticles();
        Destroy(gameObject);
    }
}
