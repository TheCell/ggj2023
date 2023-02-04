using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAction : MonoBehaviour
{
    [SerializeField] private TapActionConfig tapActionConfig;
    private int healAmount;

    void Start()
    {
        ApplyConfig(tapActionConfig);
    }

    public void tapAction()
    {
        if (GetComponent<Tree>())
        {
            this.GetComponent<Health>().Heal(healAmount);
            this.GetComponent<Tree>().SetSize();
        }
        else if (GetComponent<Crossroad>())
        {
            if(this.GetComponent<Crossroad>().HasTree())
            {
                this.GetComponentInChildren<Health>().Heal(healAmount);
                this.GetComponentInChildren<Tree>().SetSize();
            } 
            else
            {
                this.GetComponent<Crossroad>().PrepareTree();
            }
        }
    }

    private void ApplyConfig(TapActionConfig tapActionConfig)
    {
        healAmount= tapActionConfig.healAmount;
    }
}
