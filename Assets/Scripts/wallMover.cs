using UnityEngine;

public class wallMover : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveSpeed = 2f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private float moveTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
    }

    private void FixedUpdate()
    {
        moveTime += Time.fixedDeltaTime;

        Vector3 position = startPosition;
        position.x += Mathf.Sin(moveTime * moveSpeed) * moveDistance;
        rb.MovePosition(position);
    }
}
