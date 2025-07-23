using UnityEngine;

public class MesscotManager : MonoBehaviour
{
    public int path = 1;

    private void Awake()
    {
        GetComponent<Animator>().SetInteger("Path", path);
    }

    public void SpawnStainDroplet(GameObject stainDropletPrefab)
    {

        GameObject stainDroplet = Instantiate(stainDropletPrefab);
        stainDroplet.transform.position = transform.position;

    }


}
