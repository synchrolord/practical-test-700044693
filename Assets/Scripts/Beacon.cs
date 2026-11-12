using UnityEngine;

public class Beacon : MonoBehaviour
{
    public Renderer beaconRenderer;
    public Material offMaterial;
    public Material onMaterial;

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
        activated = true;
        beaconRenderer.material = onMaterial;

        Debug.Log("Beacon ON!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
            return;

        if (other.gameObject.layer ==
            LayerMask.NameToLayer("Player"))
        {
            GameEvents.Win();
        }
    }
}