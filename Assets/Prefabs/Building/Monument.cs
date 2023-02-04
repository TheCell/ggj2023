using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    public void WinGame()
    {
        Debug.Log("You won!");
        GameSceneSwitcher sceneSwitcher = gameObject.AddComponent<GameSceneSwitcher>();
        sceneSwitcher.SwitchToWinScene();
    }
}
