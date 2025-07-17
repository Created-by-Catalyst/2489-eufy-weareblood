using UnityEngine;


public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform cameraPoint;


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPoint.position, Time.deltaTime * 5);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraPoint.rotation, Time.deltaTime * 4);
    }

}

