using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float gravity = -9.81f;

    private Vector2 moveInput;
    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    public PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction sprintAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        bool isSprinting = sprintAction.IsPressed();

        float currentSpeed = isSprinting ? runSpeed : walkSpeed;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Animate blend tree
        float animationSpeed = input.magnitude * (isSprinting ? 2f : 1f); // 0 to 2
        animator.SetFloat("Speed", animationSpeed);
    }
}
