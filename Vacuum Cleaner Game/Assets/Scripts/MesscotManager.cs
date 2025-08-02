using UnityEngine;

public class MesscotManager : MonoBehaviour
{
    [HideInInspector]
    public int path = 3;

    [SerializeField]
    GameObject particles;

    public Animator messcotAnimator;

    private void Awake()
    {
        GetComponent<Animator>().SetInteger("Path", path);
    }

    public void SpawnStainDroplet(GameObject stainDropletPrefab)
    {
        Instantiate(particles, transform.position, transform.rotation);
        GameObject stainDroplet = Instantiate(stainDropletPrefab);
        stainDroplet.transform.position = transform.position;

    }

    public void MesscotIntroAnims()
    {
        GetComponent<Animator>().Play("MesscotIntro");
        messcotAnimator.SetTrigger("Intro");

    }

    public void MesscotIntroEnded()
    {
        GameManager.instance.playerVehicles[GameManager.instance.currentSection].GetComponent<Animator>().Play("EufyIntro");
    }

    public void PlayAnimation(string name)
    {
        messcotAnimator.SetTrigger(name);
    }


    private Vector3 lastPosition;
    float rotationSpeed = 10f;


    private float lastYRotation;
    public enum RotationDirection { Forward, Left, Right }
    public RotationDirection currentDirection;


    void Start()
    {
        lastY = transform.eulerAngles.y;
        lastPosition = transform.position;
    }

    float maxRotationSpeed = 90f; // degrees per second
    float left;  // 0 to 1
    float right; // 0 to 1

    private float lastY;


    public float slerpSpeed = 5f;
    public float smoothingSpeed = 7f;

    private Quaternion lastRotation;

    float maxSpeed = 10f;
    float normalizedSpeed;

    void FixedUpdate()
    {
        //GET SPEED
        //Vector3 delta = transform.position - lastPosition;
        //float speed = delta.magnitude / Time.deltaTime;

        //normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);


        //messcotAnimator.SetLayerWeight(4, normalizedSpeed / 2);


        //ROTATE BASED ON DIRECTION
        Vector3 direction = transform.position - lastPosition;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        lastPosition = transform.position;




        //ANIMATION BASED ON ROTATION

        Quaternion currentRotation = transform.rotation;
        Quaternion smoothedRotation = Quaternion.Slerp(lastRotation, currentRotation, Time.deltaTime * slerpSpeed);

        // Compute the signed delta angle on the Y axis
        float lastY = lastRotation.eulerAngles.y;
        float currentY = smoothedRotation.eulerAngles.y;

        float deltaY = Mathf.DeltaAngle(lastY, currentY) * 20;

        // Normalize delta to [-1, 1]
        float normalized = Mathf.Clamp(deltaY / maxRotationSpeed, -1f, 1f);


        float targetLeft = Mathf.Clamp01(-normalized);
        float targetRight = Mathf.Clamp01(normalized);

        // Assign left/right strengths
        left = Mathf.Lerp(left, targetLeft, Time.deltaTime * smoothingSpeed);
        right = Mathf.Lerp(right, targetRight, Time.deltaTime * smoothingSpeed);

        // Store the smoothed rotation for next frame
        lastRotation = smoothedRotation;

        //messcotAnimator.SetLayerWeight(3, 1 - (left + right));

        messcotAnimator.SetLayerWeight(2, left);
        messcotAnimator.SetLayerWeight(3, right);

    }

}
