using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength = 15f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer !=
            LayerMask.NameToLayer("Player"))
            return;

        Rigidbody rb = collision.rigidbody;

        if (rb == null)
            return;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = bounceStrength;
        rb.linearVelocity = velocity;

        Debug.Log("bounced");
    }
}