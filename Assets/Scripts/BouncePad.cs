using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength = 6f;
    public AudioClip bounceSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer !=
            LayerMask.NameToLayer("Player"))
            return;

        Rigidbody rb = collision.rigidbody;

        if (rb == null || rb.isKinematic)
            return;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = bounceStrength;
        rb.linearVelocity = velocity;

        if (bounceSound != null)
        {
            AudioSource.PlayClipAtPoint(
                bounceSound,
                transform.position
            );
        }

        Debug.Log("bounced");
    }
}