using System;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;
    [SerializeField] private float timeBetweenChange = 2f;

    private float lastColorChangeTimeStamp = 0f;
    private Renderer renderer;
    private int currentColor = 0;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Time.time > lastColorChangeTimeStamp + timeBetweenChange)
        {
            lastColorChangeTimeStamp = Time.time;
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        if (currentColor == 0)
        {
            renderer.material.color = firstColor;
            currentColor = 1;
        }
        else
        {
            renderer.material.color = secondColor;
            currentColor = 0;
        }
    }
}
