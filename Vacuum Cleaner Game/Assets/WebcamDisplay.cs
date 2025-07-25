using UnityEngine;
using UnityEngine.UI;

public class WebcamDisplay : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    [SerializeField]
    RawImage rawImage;

    void Start()
    {

        // Use the default webcam (index 0)
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            string camName = devices[0].name;
            webcamTexture = new WebCamTexture(camName);

            rawImage.texture = webcamTexture;
            rawImage.material.mainTexture = webcamTexture;

            webcamTexture.Play();
        }
        else
        {
            Debug.LogWarning("No webcam detected.");
        }
    }

    void OnDisable()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
