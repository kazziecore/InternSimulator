using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.Rendering;
// OBSOLETE FOR NOW CUZ IT WORKS LIKE SHIT. WILL FIGURE IT OUT LATER
public class playerMovement : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    public float maximumSpeed;
    public float rotationSpeed;

    private bool isGrabbing;

    private Animator animator;
    private CharacterController characterController;

    float sampleDistance = 0.5f;
    LayerMask groundLayer;

    public static event System.Action<Vector3> OnGroundTouch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void OnGrab(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            isGrabbing = true;
        }
        else
        {
            isGrabbing = false;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = maximumSpeed;

        
    }

    // Update is called once per frame
    void Update()
    {

        // point - click stuff in here

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, sampleDistance, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(navMeshHit.position);

                    OnGroundTouch?.Invoke(navMeshHit.position);
                }
            }
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) == false && Input.GetKey(KeyCode.RightShift) == false)
        {
            inputMagnitude /=2;
        }
        animator.SetFloat("inputMagnitude", inputMagnitude, 0.1f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();

       // characterController.SimpleMove(movementDirection * speed);

        if (movementDirection != Vector3.zero)
        {
        
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    float currentLayerweight = animator.GetLayerWeight(1);
    float targetLayerWeight;

    if (isGrabbing)
        {
            targetLayerWeight = 1;
        }
        else
        {
            targetLayerWeight = 0;
        }
    
    float newLayerWeight = Mathf.MoveTowards(
        currentLayerweight,
        targetLayerWeight,
        Time.deltaTime * 5);
    
    animator.SetLayerWeight(1, newLayerWeight);
    }
    




}

