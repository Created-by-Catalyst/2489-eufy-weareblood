using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    VehicleController vehicleController;


    [SerializeField]
    Camera playerCamera;

    float normalFOV = 60;
    float fastFOV = 75;

    Coroutine cameraFovCoroutine;

    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stain")
        {
            StainManager stainManager = other.gameObject.GetComponentInParent<StainManager>();

            stainManager.CleanStain();
            GameManager.instance.AddScore(stainManager.scoreValue);

            StartCoroutine(PickupStain());
            if (cameraFovCoroutine != null) StopCoroutine(cameraFovCoroutine);
            cameraFovCoroutine = StartCoroutine(CameraFovPull());
        }
    }

    IEnumerator CameraFovPull()
    {
        float time = 0.4f;
        float elapsed = 0f;

        float currentFov = playerCamera.fieldOfView;

        while (elapsed < time)
        {
            playerCamera.fieldOfView = Mathf.Lerp(currentFov, fastFOV, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.3f);


        time = 0.8f;
        elapsed = 0f;

        while (elapsed < time)
        {
            playerCamera.fieldOfView = Mathf.Lerp(fastFOV, normalFOV, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

    }



    IEnumerator PickupStain()
    {
        vehicleController.NitrousInput = 1;
        yield return new WaitForSeconds(0.1f);
        vehicleController.NitrousInput = 0;

    }

}

