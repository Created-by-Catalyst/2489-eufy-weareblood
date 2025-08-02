using UnityEngine;

public class StainDroplet : MonoBehaviour
{

    [SerializeField]
    GameObject stainPrefab;


    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -350, 0));
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Vehicle") return;
        if (other.gameObject.layer == 13) return;

        GameObject stain = Instantiate(stainPrefab);

        Vector3 contactPoint = other.ClosestPoint(transform.position);

        //float stainWidth = stainPrefab.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size.y;

        stain.transform.position = contactPoint + new Vector3(0, -0.2f, 0);

        Destroy(gameObject);
    }
}
