using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHpbar :SingletonBase<EnemyHpbar>
{
    RawImage hpBarBG;
    Image hpBar;
    void Start()
    {
        hpBarBG = GetComponent<RawImage>();
        hpBar = GetComponentInChildren<Image>();
        TurnOff();
    }

    // Update is called once per frame
    public void RenewGauge(float ratio)
    {
        hpBar.fillAmount = ratio;
    }
    public bool GetActiveSelf()
    {
        return !hpBar.enabled && !hpBarBG.enabled;
    }
    public void TurnOn()
    {
        hpBar.enabled = true;
        hpBarBG.enabled = true;
    }
    public void TurnOff()
    {
        hpBar.enabled = false;
        hpBarBG.enabled = false;
    }
}
