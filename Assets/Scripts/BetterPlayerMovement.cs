using UnityEngine;
using UnityEngine.InputSystem;
// this doesnt have all the neccessary stuff for the assignment before but it works better so im using it
public class BetterPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 200f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator animator;

    private bool isGrabbing;
    private float yRotation;
    private float yVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnGrab(InputValue value)
    {
        isGrabbing = value.isPressed;
    }

    void Update()
    {
        // mouse view stuff
        float mouseX = Mouse.current.delta.ReadValue().x * mouseSensitivity * Time.deltaTime;
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // movementttt
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        float inputMagnitude = Mathf.Clamp01(move.magnitude);

        // running
        bool isRunning = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
        float speedMultiplier = isRunning ? 1f : 0.5f;

        move *= moveSpeed * speedMultiplier;

        // gravity typeshi
        if (controller.isGrounded && yVelocity < 0)
            yVelocity = -2f;

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move + Vector3.up * yVelocity;

        controller.Move(velocity * Time.deltaTime);

        // animation stuff
        animator.SetFloat("inputMagnitude", inputMagnitude * speedMultiplier, 0.1f, Time.deltaTime);

        // grabby grabby
        float targetWeight = isGrabbing ? 1f : 0f;

        float newWeight = Mathf.MoveTowards(
            animator.GetLayerWeight(1),
            targetWeight,
            Time.deltaTime * 5f
        );

        animator.SetLayerWeight(1, newWeight);
    }
}
