using UnityEngine;
using UnityEngine.UI;

public class WebcamDisplay : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    [SerializeField]
    RawImage rawImage;

    [SerializeField]
    RawImage capturedImageDisplay;

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


    public void CaptureImage()
    {
        // Create Texture2D the size of the webcam feed
        Texture2D snapshot = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);

        // Read pixels from webcam and apply them to Texture2D
        snapshot.SetPixels(webcamTexture.GetPixels());
        snapshot.Apply();

        // Assign the captured texture to the other RawImage
        capturedImageDisplay.texture = snapshot;
    }

    void OnDisable()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
