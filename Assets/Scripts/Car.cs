using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject[] points;
    private GameObject spawnPoint;
    private GameObject moveToPoint;


    private void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        spawnPoint = points[Random.Range(0, points.Length)];
        moveToPoint = spawnPoint.GetComponent<PointSettings>().nextPoints[0];
        transform.position = spawnPoint.transform.position;
        transform.rotation = spawnPoint.transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveToPoint.transform.position, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, moveToPoint.transform.rotation, 0.03f);

        if (Vector3.Distance(transform.position, moveToPoint.transform.position) < 0.1f)
        {
            moveToPoint = moveToPoint.GetComponent<PointSettings>().nextPoints[0];
        }
        //transform.position = Vector3.Lerp(transform.position, test, 0.01f);
    }
}
