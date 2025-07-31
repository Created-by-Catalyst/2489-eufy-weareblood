using UnityEngine;

public class MesscotManager : MonoBehaviour
{
    [HideInInspector]
    public int path = 3;

    public Animator messcotAnimator;

    private void Awake()
    {
        GetComponent<Animator>().SetInteger("Path", path);
    }

    public void SpawnStainDroplet(GameObject stainDropletPrefab)
    {

        GameObject stainDroplet = Instantiate(stainDropletPrefab);
        stainDroplet.transform.position = transform.position;

    }

    public void PlayAnimation(string name)
    {
        messcotAnimator.SetTrigger(name);
    }


    private Vector3 lastPosition;
    float rotationSpeed = 30f;


    private float lastYRotation;
    public enum RotationDirection { Forward, Left, Right }
    public RotationDirection currentDirection;

    float threshold = 0.8f; // minimum angle delta to count as turning

    void Start()
    {
        lastYRotation = transform.eulerAngles.y;
        lastPosition = transform.position;
    }




    void FixedUpdate()
    {
        Vector3 direction = transform.position - lastPosition;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        lastPosition = transform.position;



        float currentY = transform.eulerAngles.y;
        float deltaY = Mathf.DeltaAngle(lastYRotation, currentY); // handles wraparound

        if (Mathf.Abs(deltaY) < threshold)
        {
            currentDirection = RotationDirection.Forward;
            messcotAnimator.SetInteger("Direction", 0);
        }
        else if (deltaY > 0)
        {
            currentDirection = RotationDirection.Right;
            messcotAnimator.SetInteger("Direction", 1);
        }
        else
        {
            currentDirection = RotationDirection.Left;
            messcotAnimator.SetInteger("Direction", 2);
        }

        lastYRotation = currentY;
    }

}
