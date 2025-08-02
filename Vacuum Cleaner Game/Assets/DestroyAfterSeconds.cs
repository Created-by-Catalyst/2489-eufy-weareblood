using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float seconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("DestroySelf", seconds);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }


}
