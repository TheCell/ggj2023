using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    public string goToScene;

    [SerializeField]
    public Image blinkingImage = null;

    float timer = 0;
    float speed = 18;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.anyKey)
        {
            SceneManager.LoadScene(goToScene);
        }

        if (blinkingImage)
        {
            var resultOfCrazyMath = ((Mathf.Cos(timer * speed / Mathf.PI) * 1) + 1) / 2;

            var color = blinkingImage.color;
            color.a = resultOfCrazyMath;
            blinkingImage.color = color;
        }
    }
}
