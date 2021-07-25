using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 origin;

    public Vector2 velocity;
    public float speed;
    public float jumpspeed;
    public bool j;

    public bool MoveImpulse = true;

    [SerializeField] ParticleSystem JumpParticles;

    public static PlayerController instance { get; private set; }
    private void Awake() => instance = this;
    public void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Respawn();
    }
    public void SetJumpVelocity()
    {
        velocity.y = 1;
    }
    public void SetMovementVelocity(float x)
    {
        velocity.x = x;
    }
    void Update()
    {
        if (GameStateController.gameState == GameStateController.State.Playing)
        {
            if (MoveImpulse) rb.AddForce(Vector2.right * velocity.x * speed);
            else rb.velocity = new Vector2(velocity.x * speed * 1.75f, rb.velocity.y);
            
            if (velocity.y != 0)
            {
                rb.AddForce(Vector2.up * velocity.y * jumpspeed);
                velocity.y = 0;

                JumpParticles.transform.localEulerAngles = new Vector3(90, 90, 0);
                JumpParticles.Play();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameStateController.ExitGame();
        GameStateController.ReloadScene();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameStateController.ExitGame();
        GameStateController.ReloadScene();
    }
    public void Respawn()
    {
        velocity = Vector2.zero;
        transform.position = origin;
    }
}