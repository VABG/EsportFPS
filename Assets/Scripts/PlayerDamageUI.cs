using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageUI : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image dmgImage;
    [SerializeField] float dmgTimeMult = 2.0f;

    bool animating = false;
    float alpha = 0;
    bool dead = false;
    void Start()
    {
        SetColorAlpha(0);
    }

    public void Damage(float alpha)
    {
        if (!dead)
        {
            this.alpha = alpha;
            animating = true;
        }
    }

    public void Die()
    {
        dead = true;
        animating = false;
        SetColorAlpha(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            alpha -= Time.deltaTime * dmgTimeMult;
            if (alpha <= 0)
            {
                alpha = 0;
                animating = false;
            }
            SetColorAlpha(alpha);
        }
    }

    void SetColorAlpha(float alpha)
    {
        dmgImage.color = new Color(dmgImage.color.r, dmgImage.color.g, dmgImage.color.b, alpha);
    }
}
