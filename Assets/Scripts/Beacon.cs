using UnityEngine;

public class Beacon : MonoBehaviour
{
    public Renderer beaconRenderer;
    public Material offMaterial;
    public Material onMaterial;
    public AudioClip activateSound;

    private bool activated = false;

    private void OnEnable()
    {
        GameEvents.BeaconActivated += Activate;
    }

    private void OnDisable()
    {
        GameEvents.BeaconActivated -= Activate;
    }

    private void Start()
    {
        beaconRenderer.material = offMaterial;
    }

    private void Activate()
    {
        if (activated)
            return;

        activated = true;
        beaconRenderer.material = onMaterial;

        if (activateSound != null)
        {
            AudioSource.PlayClipAtPoint(
                activateSound,
                transform.position
            );
        }

        Debug.Log("Beacon ON!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
            return;

        if (other.gameObject.layer ==
            LayerMask.NameToLayer("Player"))
        {
            if (other.attachedRigidbody == null ||
                other.attachedRigidbody.isKinematic)
                return;

            GameEvents.Win();
        }
    }
}