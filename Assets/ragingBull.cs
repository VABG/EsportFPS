using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragingBull : MonoBehaviour
{
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
    [SerializeField] float shotLifeTime = 5.0f;
    [SerializeField] Light shotLight;
    [SerializeField] float shotLightLifeTime = .05f;
    [SerializeField] List<GameObject> bulletsVisual;
    private float shotLightLifeCounter = 100;
    public bool SlowMoReload = false;
    Animator anim;
    const int ammoMax = 6;
    int ammo = 6;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        shotLightLifeCounter += Time.deltaTime;
        if (shotLightLifeCounter > shotLightLifeTime) shotLight.enabled = false;
    }


    public void TryShoot()
    {
        AnimatorStateInfo aInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (aInfo.IsName("BullIdle"))
        {
            if (ammo > 0)
            {
                anim.SetTrigger("Shot");
                ammo--;
            }
            else
            {
                anim.SetTrigger("ShootDry");
            }
        }
    }

    public void LookAtGun()
    {
        AnimatorStateInfo aInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (aInfo.IsName("BullIdle"))
        {
            anim.SetTrigger("LookAtGun");
        }
    }

    public void Reload()
    {
        AnimatorStateInfo aInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (aInfo.IsName("BullIdle"))
        {
            if (ammo != ammoMax)
            {
                if (SlowMoReload) Time.timeScale = .1f;

                anim.SetTrigger("Reload");
            }
            else
            {
                anim.SetTrigger("CheckAmmo");
            }
        }
    }

    public void ShootBullet()
    {
        GameObject g = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Destroy(g, shotLifeTime);
        shotLight.enabled = true;
        shotLightLifeCounter = 0;
    }

    public void ShowBulletsInCasings()
    {
        ammo = ammoMax;
        if (SlowMoReload) Time.timeScale = 1.0f;
        foreach (GameObject g in bulletsVisual)
        {
                g.SetActive(true);            
        }
    }

    public void HideBulletsInCasings()
    {
        for (int i = ammoMax-1; i > ammo-1; i--)
        {
            bulletsVisual[i].SetActive(false);
        }
    }
}
