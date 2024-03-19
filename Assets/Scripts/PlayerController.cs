using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Vector3 origin;

    public bool isAlive = true;

    public Vector2 velocity;
    public float speed;
    public float jumpspeed;

    public bool MoveImpulse = true;

    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] GameObject ballObject;
    [SerializeField] GameObject trailEffectObject;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound1;
    [SerializeField] AudioClip deathSound2;

    public void Init()
    {
        origin = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
        Disactivate();
        Respawn();
    }
    public void Activate()
    {
        rigidbody2d.simulated = true;
        isAlive = true; 
    }
    public void Disactivate()
    {
        rigidbody2d.simulated = false;
        deathParticles.Stop();
        deathParticles.Clear();
        jumpParticles.Stop();
        jumpParticles.Clear();
    }

    public void SetJumpVelocity()
    {
        velocity.y = 1;
    }
    public void SetMovementVelocity(float x)
    {
        velocity.x = x;
    }
    private void Jump()
    {
        rigidbody2d.AddForce(Vector2.up * velocity.y * jumpspeed);
        velocity.y = 0;

        jumpParticles.Play();
        Environment.audioManager.PlayClip(jumpSound, 1.15f);
    }
    void Update()
    {
        if (Environment.gameStateController.gameState == GameStateController.State.Playing)
        {
            if (MoveImpulse) rigidbody2d.AddForce(Vector2.right * velocity.x * speed);
            else rigidbody2d.velocity = new Vector2(velocity.x * speed * 1.75f, rigidbody2d.velocity.y);
            
            if (velocity.y != 0)
            {
                Jump();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Die();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Die();
    }

    private void Die()
    {
        if (!isAlive) return;
        isAlive = false;

        Environment.audioManager.PlayClip(deathSound1, 0.65f);
        Environment.audioManager.PlayClip(deathSound2, 0.65f);
        deathParticles.transform.rotation = Quaternion.FromToRotation(-deathParticles.transform.up, transform.position + new Vector3(rigidbody2d.velocity.x, rigidbody2d.velocity.y)); // Quaternion.LookRotation(Vector3.forward, deathParticles.transform.);
        //deathParticles.transform.LookAt(transform.position + new Vector3(rigidbody2d.velocity.x, rigidbody2d.velocity.y) * 3, -deathParticles.transform.up);
        
        deathParticles.Play();
        ballObject.SetActive(false);
        trailEffectObject.SetActive(false);

        Environment.gameStateController.Die();
    }
    public void Respawn()
    {
        velocity = Vector2.zero;
        transform.position = origin;
    }
}