using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Color mainColor;
    private Renderer playerRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mainColor = new Color(
      Random.Range(0f, 1f),
      Random.Range(0f, 1f),
      Random.Range(0f, 1f)
  );
        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = mainColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
