using UnityEngine;

public class Lava : MonoBehaviour
{
    public Transform startPad;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer !=
            LayerMask.NameToLayer("Player"))
            return;

        Rigidbody rb = other.attachedRigidbody;

        if (rb == null || rb.isKinematic) return;

        Debug.Log("Burned");

        rb.position = startPad.position + (Vector3.up * 2f);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        GameEvents.PlayerBurned();
    }
}