using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHpbar : SingletonBase<PlayerHpbar>
{
    Image hpBar;

    void Start()
    {
        hpBar = GetComponent<Image>();
    }

    // Update is called once per frame
    public void RenewGauge(float ratio)
    {
        hpBar.fillAmount = ratio;
    }
}
