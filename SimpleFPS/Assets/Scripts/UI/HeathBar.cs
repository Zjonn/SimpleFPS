
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public Image bar;

    int maxHP;

    public void Init(int maxHP)
    {
        bar.fillAmount = 1;
        this.maxHP = maxHP;
    }

    public void UpdateHP(float hp)
    {
        bar.fillAmount = hp / maxHP;
    }
}
