using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector] public bool IsMoving;
    [SerializeField] private float speed;

    private PointSettings pointSettings;
    private Renderer carRenderer;

    private GameObject[] allPoints;
    private GameObject carSpawnPoint;
    private GameObject moveToPoint;

    private void Start()
    {
        carRenderer = GetComponent<Renderer>();
        carRenderer.material.color = new Color(Random.value, Random.value, Random.value, 1); // Set random color

        allPoints = GameObject.FindGameObjectsWithTag("Point");
        IsMoving = true;

        SetSpawnPoint();
    }

    private void FixedUpdate()
    {
        CheckObstacle();
        Moving();
    }

    private void CheckObstacle()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < 3)
            {
                if (hit.collider.gameObject.CompareTag("Car") || hit.collider.gameObject.CompareTag("TrafficLight"))
                {
                    IsMoving = false;
                }
            }
        }
        else
        {
            IsMoving = true;
        }
    }

    private void Moving()
    {
        if (IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint.transform.position, speed);

            if (Vector3.Distance(transform.position, moveToPoint.transform.position) < 0.1f)
            {
                ChangeNextPoint();
            }
        }
    }

    private void ChangeNextPoint()
    {
        pointSettings = moveToPoint.GetComponent<PointSettings>();

        if (pointSettings.NextPoints.Length == 1)
        {
            moveToPoint = pointSettings.NextPoints[0];  // Moving forward
        }
        else
        {
            moveToPoint = pointSettings.NextPoints[Random.Range(0, pointSettings.NextPoints.Length)]; // Set random distination 
        }
        transform.LookAt(moveToPoint.transform.position);
    }

    private void SetSpawnPoint()
    {
        carSpawnPoint = allPoints[Random.Range(0, allPoints.Length)];
        moveToPoint = carSpawnPoint.GetComponent<PointSettings>().NextPoints[0];

        transform.position = carSpawnPoint.transform.position;
        transform.LookAt(moveToPoint.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Destroy(gameObject);
        }
    }
}
