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
    private GameObject car;

    private void Start()
    {
        lamp_renderer = lamp.GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
        StartCoroutine(TrafficLightTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGreenColor)
        {
            car = other.gameObject;
            car.GetComponent<Car>().IsMoving = false;
        }
    }

    private IEnumerator TrafficLightTimer()
    {
        ChangeLampColor();
        yield return new WaitForSeconds(timeToChangeColor);
        isGreenColor = !isGreenColor;

        if(car != null)
        {
            car.GetComponent<Car>().IsMoving = true;
            car = null;
        }

        StartCoroutine(TrafficLightTimer());
    }

    private void ChangeLampColor()
    {
        lamp_renderer.material.color = isGreenColor ? Color.green : Color.red;
        collider.enabled = isGreenColor ? false : true;
    }
}
