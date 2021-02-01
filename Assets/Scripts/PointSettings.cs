using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSettings : MonoBehaviour
{
    public GameObject[] NextPoints
    {
        get
        {
            return nextPoints;
        }
    }

    [SerializeField] private GameObject[] nextPoints;
}
