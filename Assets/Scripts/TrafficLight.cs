using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private float timeToChangeColor;
    [SerializeField] private bool isGreenColor;
    [SerializeField] private GameObject lamp;

    private Renderer lamp_renderer;
    private GameObject car;

    private void Start()
    {
        lamp_renderer = lamp.GetComponent<Renderer>();
        StartCoroutine(TimerToChangeColor());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGreenColor)
        {
            car = other.gameObject;
            car.GetComponent<Car>().IsMoving = false;
        }
    }

    private IEnumerator TimerToChangeColor()
    {
        ChangeLampColor();
        yield return new WaitForSeconds(timeToChangeColor);
        isGreenColor = !isGreenColor;

        if(car != null)
        {
            car.GetComponent<Car>().IsMoving = true;
            car = null;
        }

        StartCoroutine(TimerToChangeColor());
    }

    private void ChangeLampColor()
    {
        lamp_renderer.material.color = isGreenColor ? Color.green : Color.red;
    }
}
