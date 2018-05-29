
using UnityEngine;
using UnityEngine.UI;

public class FollowingHealthBar : MonoBehaviour
{
    public Image bar;
    public float barHeight;
   
    Transform toFollow;
    Transform lookAt;

    int maxHP;

    const float filledBar = 1;

    public void Init(Transform toFollow, Transform lookAt, int maxHP)
    {
        this.toFollow = toFollow;
        this.lookAt = lookAt;
        bar.fillAmount = filledBar;
        this.maxHP = maxHP;
    }

    public void UpdateHP(float hp)
    {
        bar.fillAmount = hp / maxHP;
    }

    private void Update()
    {
        if (toFollow)
        {
            Vector3 pos = toFollow.position;
            pos.y += barHeight;
            transform.position = pos;
            transform.LookAt(lookAt.position);
        }
        else
            Destroy(gameObject);
    }
}
