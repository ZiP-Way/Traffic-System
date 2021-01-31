using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private float timeToChangeColor;
    [SerializeField] private bool isGreenColor;
    [SerializeField] private GameObject lamp;

    private Renderer lamp_renderer;

    private void Start()
    {
        lamp_renderer = lamp.GetComponent<Renderer>();
        StartCoroutine(TimerToChangeColor());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Gi");
    }

    private IEnumerator TimerToChangeColor()
    {
        ChangeLampColor();
        yield return new WaitForSeconds(timeToChangeColor);
        isGreenColor = !isGreenColor;
        StartCoroutine(TimerToChangeColor());
    }

    private void ChangeLampColor()
    {
        lamp_renderer.material.color = isGreenColor ? Color.green : Color.red;
    }
}
