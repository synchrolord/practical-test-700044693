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
    public AudioClip jumpSound;
    
    
    private Rigidbody rb;
    private Vector2 moveinput;
    private bool jumpRequisted;
    private AudioSource audioSource;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.playOnAwake = false;

        Physics.gravity = new Vector3(
            0f, -moonSettinggs.gravity, 0f
        );
    }


    private void OnEnable()
    {
       moveAction.action.Enable();
       jumpAction.action.Enable();
       jumpAction.action.performed += OnJump;
       GameEvents.Won += StopPlayer;
       GameEvents.Lost += StopPlayer;

    }


    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJump;
        GameEvents.Won -= StopPlayer;
        GameEvents.Lost -= StopPlayer;

    }


    private void Update()
    {
        if (rb.isKinematic)
            return;

        moveinput = moveAction.action.ReadValue<Vector2>();
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        if (!rb.isKinematic && IsGrounded())
            jumpRequisted = true;
    }



    private void FixedUpdate()
    {
        if (rb.isKinematic)
            return;

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

            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
        }

        jumpRequisted = false;
    }

    
    private void StopPlayer()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        moveinput = Vector2.zero;
        jumpRequisted = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }


    private bool IsGrounded()
    {
        return rb.linearVelocity.y <= 0.05f && Physics.Raycast(
            transform.position,
            Vector3.down,
            moonSettinggs.GroundCheck,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }
    


}
