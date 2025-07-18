using UnityEngine;

public class StainDroplet : MonoBehaviour
{

    [SerializeField]
    GameObject stainPrefab;


    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 350, 0));
    }


    private void OnTriggerEnter(Collider other)
    {
        print(other);
        GameObject stain = Instantiate(stainPrefab);

        stain.transform.position = new Vector3(transform.position.x, 1.66f, transform.position.z);

        Destroy(gameObject);
    }
}
