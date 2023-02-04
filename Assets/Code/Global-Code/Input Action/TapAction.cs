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

    private void tapAction()
    {
        if (GetComponent<Tree>())
        {
            this.GetComponent<Health>().Heal(healAmount);
            this.GetComponent<Tree>().SetSize();
        }
        else if (GetComponent<Crossroad>())
        {
            this.GetComponent<Crossroad>().PrepareTree();
        }
    }

    private void ApplyConfig(TapActionConfig tapActionConfig)
    {
        healAmount= tapActionConfig.healAmount;
    }
}
