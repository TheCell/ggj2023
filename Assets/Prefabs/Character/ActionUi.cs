using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionUiType
{
    Left, Right, Top, Bottom
}

public class ActionUi : MonoBehaviour
{
    public Sprite activeSprite;
    [SerializeField]
    public ActionUiType type;

    private Sprite originalSprite;

    public void Start()
    {
        originalSprite = this.GetComponent<SpriteRenderer>().sprite;
    }

    public void Select()
    {
        switch (type)
        {
            case ActionUiType.Top:
                Debug.Log("top");
                break;
            case ActionUiType.Right:
                Debug.Log("right");
                break;
            case ActionUiType.Bottom:
                Debug.Log("bottom");
                break;
            case ActionUiType.Left:
                Debug.Log("left");
                break;
        }
        Passivate();
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().sprite = activeSprite;
    }

    public void Passivate()
    {
        this.GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
