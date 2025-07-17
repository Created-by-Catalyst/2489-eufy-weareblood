using UnityEngine;


public class StainManager : MonoBehaviour
{

    public int scoreValue = 10;

    public void CleanStain()
    {
        Destroy(gameObject);
    }
}

