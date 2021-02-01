using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private float timeToChangeColor;
    [SerializeField] private bool isGreenColor;
    [SerializeField] private GameObject lamp;

    private Renderer lamp_renderer;
    private BoxCollider collider;

    private void Start()
    {
        lamp_renderer = lamp.GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
        StartCoroutine(TrafficLightTimer());
    }

    private IEnumerator TrafficLightTimer()
    {
        ChangeLampColor();
        yield return new WaitForSeconds(timeToChangeColor);
        isGreenColor = !isGreenColor;

        StartCoroutine(TrafficLightTimer());
    }

    private void ChangeLampColor()
    {
        lamp_renderer.material.color = isGreenColor ? Color.green : Color.red;
        collider.enabled = isGreenColor ? false : true;
    }
}
