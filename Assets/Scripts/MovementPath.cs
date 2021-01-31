using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField] private GameObject[] pathElements;
    private PointSettings pointSettings;
    private GameObject[] nextPoint;

    private void OnDrawGizmos()
    {
        if (pathElements != null)
        {
            for (int i = 0; i < pathElements.Length; i++)
            {
                pointSettings = pathElements[i].GetComponent<PointSettings>();
                nextPoint = pointSettings.nextPoints;

                if (nextPoint != null)
                {
                    if (nextPoint.Length == 1)
                    { // Default point
                        Gizmos.DrawLine(pathElements[i].transform.position, nextPoint[0].transform.position);
                    }
                    else
                    {
                        for (int j = 0; j < nextPoint.Length; j++)
                        { // Branching point
                            Gizmos.DrawLine(pathElements[i].transform.position, nextPoint[j].transform.position);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Points not founded");
        }
    }
}
