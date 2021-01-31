using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector] public bool IsMoving;
    [SerializeField] private float speed;

    private PointSettings pointSettings;

    private GameObject[] allPoints;
    private GameObject carSpawnPoint;
    private GameObject moveToPoint;


    private void Start()
    {
        allPoints = GameObject.FindGameObjectsWithTag("Point");
        IsMoving = true;

        SetSpawnPoint();
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint.transform.position, speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveToPoint.transform.rotation, 0.03f);

            if (Vector3.Distance(transform.position, moveToPoint.transform.position) < 0.1f)
            {
                pointSettings = moveToPoint.GetComponent<PointSettings>();
                if (pointSettings.nextPoints.Length == 1)
                {
                    moveToPoint = pointSettings.nextPoints[0];
                }
                else
                {
                    moveToPoint = pointSettings.nextPoints[Random.Range(0, pointSettings.nextPoints.Length)];
                }
            }
        }
    }

    private void SetSpawnPoint()
    {
        carSpawnPoint = allPoints[Random.Range(0, allPoints.Length)];
        moveToPoint = carSpawnPoint.GetComponent<PointSettings>().nextPoints[0];
        transform.position = carSpawnPoint.transform.position;
        transform.rotation = carSpawnPoint.transform.rotation;
    }
}
