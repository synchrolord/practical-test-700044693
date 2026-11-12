using System;
using UnityEngine;
using UnityEngine.InputSystem;




[RequireComponent(typeof(Rigidbody))]
public class NewMonoBehaviourScript : MonoBehaviour
{

    [Header("input")]
    
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    
    [Header("Settings")]
    [SerializeField] private MoonSettinggs moonSettinggs;
    
    
    [Header("other")]
    [SerializeField] private LayerMask groundLayer;
    
    
    private Rigidbody rb;
    private Vector2 moveinput;
    private bool jumpRequisted;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Physics.gravity = new Vector3(
            0f, -moonSettinggs.gravity, 0f
        );
    }


    private void OnEnable()
    {
       moveAction.action.Enable();
       jumpAction.action.Enable();
       jumpAction.action.performed += OnJump;

    }


    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJump;

    }


    private void Update()
    {
        moveinput = moveAction.action.ReadValue<Vector2>();
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        jumpRequisted = true;
    }



    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(
            moveinput.x, 0f, moveinput.y
        );

        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 velocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            direction.x * moonSettinggs.moveSpeed,
            velocity.y,
            direction.z * moonSettinggs.moveSpeed
        );

        if (jumpRequisted && IsGrounded())
        {
            rb.AddForce(
                Vector3.up * moonSettinggs.jumpStrength,
                ForceMode.Impulse
            );
        }

        jumpRequisted = false;
    }

    
    private bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            1.1f,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }
    


}
