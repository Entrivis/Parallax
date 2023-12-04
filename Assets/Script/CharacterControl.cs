using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    //Movement
    [HideInInspector] public static float _input;
    [HideInInspector] public float absInput;
    [Header("Movement")]
    public Animator animator;
    private float speed = 5f;

    //Jump
    [HideInInspector] public bool isGrounded { get; private set; }
    [Header("JumpPhysics")]
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private float jumpForce = 15f;
    [HideInInspector] public bool isJumping { get; private set; }
    private Vector2 gravity;
    [SerializeField] private float jumpMultiplier = 1f;

    //JumpTime
    [Header("JumpTime")]
    public float jumpTime;
    private float jumpTimeCounter;

    //Enemy
    public Enemy enemy;
    public bool enemyPhysic { get; private set; }

    private Shake playerShake;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = new Vector2(0, -Physics2D.gravity.y);
        playerShake = GetComponent<Shake>();
    }
    private void Update()
    {
        Movement();
        Flip();
        Jump();
    }
    private void Movement()
    {
        _input = InputSystem.inputSystem.Movement();
        rb.velocity = new Vector2(_input * speed, rb.velocity.y);
        absInput = Mathf.Abs(_input);
    }
    private void Flip()
    {
        if (_input > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (_input < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCapsule(transform.position, GetComponent<CapsuleCollider2D>().size, 0, 0, groundLayer);
        if (isGrounded || enemyPhysic) jumpTimeCounter = 0;
        if (isGrounded && InputSystem.inputSystem.Jump() || enemyPhysic && InputSystem.inputSystem.Jump())
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isJumping)
        {
            jumpTimeCounter += Time.deltaTime;

            if (jumpTimeCounter > jumpTime)
            {

                isJumping = false;
            }
            rb.velocity += jumpMultiplier * gravity * Time.deltaTime;
        }
        if (!InputSystem.inputSystem.Jump())
        {
            isJumping = false;
            jumpTimeCounter = 0;
            if (rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyPhysic = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyPhysic = false;
        }
    }
}
