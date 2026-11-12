using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer !=
            LayerMask.NameToLayer("Player"))
            return;

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