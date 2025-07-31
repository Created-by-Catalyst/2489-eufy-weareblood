using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    VehicleController vehicleController;
    Rigidbody rigidbody;


    [SerializeField]
    Camera playerCamera;

    float normalFOV = 60;
    float fastFOV = 75;

    Coroutine cameraFovCoroutine;

    private void Awake()
    {
        vehicleController = GetComponent<VehicleController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stain")
        {
            StainManager stainManager = other.gameObject.GetComponentInParent<StainManager>();
            Destroy(other);

            rigidbody.linearVelocity = rigidbody.linearVelocity * 0.6f;

            StartCoroutine(PickupStain(stainManager));
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



    IEnumerator PickupStain(StainManager stainManager)
    {
        yield return new WaitForSeconds(0.3f);

        GameManager.instance.AddScore(stainManager.scoreValue, stainManager.stainTier, stainManager.description);
        stainManager.CleanStain();


        if (cameraFovCoroutine != null) StopCoroutine(cameraFovCoroutine);
        cameraFovCoroutine = StartCoroutine(CameraFovPull());
        vehicleController.NitrousInput = 1;
        yield return new WaitForSeconds(0.2f);
        vehicleController.NitrousInput = 0;

    }

}

