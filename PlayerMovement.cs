using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = joystick.Direction;
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    private void UpdateAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}