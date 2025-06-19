using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float dashForce = 10f;
    public float dashCooldown = 1f;
    private float lastDashTime;

    private bool isDashing = false;

    private void Awake()
    {
        playerControls = new PlayerControls();

        // Read movement input
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Dash input
        playerControls.Player.Dash.performed += ctx => TryDash();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // ?? Link to Animator
    }

    private void Update()
    {
        // Apply movement
        if (!isDashing)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }

        // Update animation parameters
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        animator.SetBool("IsDashing", isDashing);
    }

    private void TryDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && moveInput != Vector2.zero)
        {
            lastDashTime = Time.time;
            isDashing = true;

            rb.linearVelocity = Vector2.zero; // Optional: stop movement briefly
            rb.AddForce(moveInput.normalized * dashForce, ForceMode2D.Impulse);

            // Optional: stop dash after short time
            Invoke("EndDash", 0.2f); // Dash lasts 0.2 seconds
        }
    }

    private void EndDash()
    {
        isDashing = false;
    }
}
