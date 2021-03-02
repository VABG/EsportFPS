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
    private float shotLightLifeCounter = 100;
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
            anim.SetTrigger("Reload");
            ammo = ammoMax;
        }
    }

    public void ShootBullet()
    {
        GameObject g = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Destroy(g, shotLifeTime);
        shotLight.enabled = true;
        shotLightLifeCounter = 0;
    }
}
