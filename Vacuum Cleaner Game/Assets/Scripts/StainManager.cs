using UnityEngine;


public class StainManager : MonoBehaviour
{
    public int stainTier = 0;
    public int scoreValue = 10;
    public string description;
    public GameObject particles;

    public void CleanStain()
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

