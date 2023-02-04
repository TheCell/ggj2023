using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    public string goToScene;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(goToScene);
        }
    }
}
