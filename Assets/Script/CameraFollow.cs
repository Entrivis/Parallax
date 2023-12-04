using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0, 0, -10);
    private bool leftLimit, rightLimit;
    private float maximumPosition, minimumPosition;
    [SerializeField] private float smoothSpeed;
    int frameCount = 0;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftLimit"))
        {
            leftLimit = true;
        }
        if (other.gameObject.CompareTag("RightLimit"))
        {
            rightLimit = true;
        }
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, -10f);
        if (!leftLimit && !rightLimit && frameCount != 0)
        {
            Vector3 smoothMove = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothMove;
        }
        if (leftLimit && InputSystem.inputSystem.Movement() > 0)
        {
            maximumPosition = Mathf.Max(targetPosition.x, transform.position.x);
            transform.position = new Vector3(maximumPosition, targetPosition.y, targetPosition.z);
            if (targetPosition.x == maximumPosition)
            {
                leftLimit = false;
            }
        }
        if (rightLimit && InputSystem.inputSystem.Movement() < 0)
        {
            minimumPosition = Mathf.Min(targetPosition.x, transform.position.x);
            transform.position = new Vector3(minimumPosition, targetPosition.y, targetPosition.z);
            if (targetPosition.x == minimumPosition)
            {
                rightLimit = false;
            }
        }
        frameCount++;
    }

}
