using UnityEngine;

public class MesscotManager : MonoBehaviour
{
    public void SpawnStainDroplet(GameObject stainDropletPrefab)
    {

        GameObject stainDroplet = Instantiate(stainDropletPrefab);
        stainDroplet.transform.position = transform.position;

    }


}
