
using UnityEngine;
using UnityEngine.UI;

public class HpFollowingBar : MonoBehaviour
{
    public Image bar;
    public GameObject toFollow;

    int maxHP;

    const float filledBar = 1;

    public void Init(int maxHP)
    {
        bar.fillAmount = filledBar;
        this.maxHP = maxHP;
    }

    public void UpdateHP(float hp)
    {
        bar.fillAmount = hp / maxHP;
    }
}
