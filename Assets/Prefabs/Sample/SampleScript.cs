using System;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;
    [SerializeField] private float timeBetweenChange = 2f;

    private float lastColorChangeTimeStamp = 0f;
    private Renderer sampleRenderer;
    private int currentColor = 0;

    void Start()
    {
        sampleRenderer = GetComponent<Renderer>();
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
            sampleRenderer.material.color = firstColor;
            currentColor = 1;
        }
        else
        {
            sampleRenderer.material.color = secondColor;
            currentColor = 0;
        }
    }
}
