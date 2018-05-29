using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public Text hpText;

    int maxHP;

    const float filledBar = 1;

    public void Init(int maxHP)
    {
        bar.fillAmount = filledBar;
        this.maxHP = maxHP;
    }

    public void UpdateHP(float hp)
    {
        float hpFactor = hp / maxHP;
        bar.fillAmount = hpFactor;
        hpText.color = new Color(hpFactor, hpFactor, hpFactor);
    }
}
