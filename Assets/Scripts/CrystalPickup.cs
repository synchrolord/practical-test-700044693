using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    public AudioClip pickupSound;
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer !=
            LayerMask.NameToLayer("Player"))
            return;

        if (collected || other.attachedRigidbody == null ||
            other.attachedRigidbody.isKinematic)
            return;

        collected = true;
        GameEvents.CollectCrystal();

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                transform.position
            );
        }

        Destroy(gameObject);
    }
}