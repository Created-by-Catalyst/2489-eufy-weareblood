using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public VehicleController vehicleController;

    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stain")
        {
            other.gameObject.GetComponentInParent<StainManager>().CleanStain();
            StartCoroutine(PickupStain());
        }
    }

    IEnumerator PickupStain()
    {

        vehicleController.NitrousInput = 1;
        yield return new WaitForSeconds(0.5f);
        vehicleController.NitrousInput = 0;

    }

}

