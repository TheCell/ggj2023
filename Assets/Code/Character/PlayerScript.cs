using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Sprite player1;
    [SerializeField] private Sprite player2;
    [SerializeField] private Sprite player3;
    [SerializeField] private Sprite player4;

    private static int playerCount = 0;

    void Start()
    {
        var renderer = this.GetComponent<SpriteRenderer>();

        switch (playerCount)
        {
            case 0:
                renderer.sprite = player1;
                break;
            case 1:
                renderer.sprite = player2;
                break;
            case 2:
                renderer.sprite = player3;
                break;
            case 3:
                renderer.sprite = player4;
                break;
            default:
                throw new System.Exception("TOO MANY PLAYERS");
        }

        playerCount++;
    }
}
