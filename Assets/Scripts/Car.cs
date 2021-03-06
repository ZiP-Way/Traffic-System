﻿using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector] public bool IsMoving;
    [SerializeField] private float speed;
    [SerializeField] private float distanceOfDetection;

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
        IsMoving = true;

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * distanceOfDetection, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("TrafficLight"))
            {
                if (hit.distance < distanceOfDetection - 2)
                {
                    IsMoving = false;
                }
            }
            if (hit.collider.gameObject.CompareTag("Car"))
            {
                if (hit.distance < distanceOfDetection)
                {
                    IsMoving = false;
                }
            }
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
