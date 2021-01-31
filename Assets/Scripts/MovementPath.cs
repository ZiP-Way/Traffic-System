using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField] private GameObject[] pathElements;

    private PointSettings pointSettings;
    private GameObject[] pos;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < pathElements.Length; i++)
        {
            pointSettings = pathElements[i].GetComponent<PointSettings>();
            pos = pointSettings.nextPoints;
            if (pos != null)
            {
                if (pos.Length == 1)
                {
                    Gizmos.DrawLine(pathElements[i].transform.position, pos[0].transform.position);
                }
                else
                {
                    for(int j = 0; j < pos.Length; j++)
                    {
                        Gizmos.DrawLine(pathElements[i].transform.position, pos[j].transform.position);
                    }
                }
            }
        }
    }
}
