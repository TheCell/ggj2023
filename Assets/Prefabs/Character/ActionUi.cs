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

    public void Select(GameObject tree)
    {
        if(tree.GetComponent<Crossroad>())
        {
            tree.GetComponent<Crossroad>().ContextMenuAction(type);
            Passivate();
        }
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
