using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    abstract void TryShoot();
    abstract void LookAtWeapon();
    abstract void Reload();
    abstract void HideWeapon();
    abstract void ShowWeapon();
    abstract bool CanChangeWeapon();
}

public class ragingBull : MonoBehaviour, IShootable
{
    [SerializeField] GameObject shot;
    [SerializeField] GameObject bulletHole;
    [SerializeField] Transform shotSpawn;
    [SerializeField] float bulletPower = 100;
    [SerializeField] float shotLifeTime = 5.0f;
    [SerializeField] Light shotLight;
    [SerializeField] float shotLightLifeTime = .05f;
    [SerializeField] List<GameObject> bulletsVisual;
    [SerializeField] ParticleSystem shotPFX;

    private float shotLightLifeCounter = 100;
    public bool SlowMoReload = false;
    Animator anim;
    const int ammoMax = 6;
    int ammo = 6;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
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

    public void LookAtWeapon()
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
        Ray r = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit rHit;

        Physics.Raycast(r, out rHit);

        Destroy(Instantiate(shotPFX, shotSpawn.position, shotSpawn.rotation).gameObject, .2f);

        if (rHit.collider)
        {
            GameObject bh = Instantiate(bulletHole, rHit.point + rHit.normal * .001f, Quaternion.FromToRotation(Vector3.up, rHit.normal));
            bh.transform.parent = rHit.transform;
        }

        IDamageable d = rHit.collider.gameObject.GetComponent<IDamageable>();
        Vector3 force = r.direction * bulletPower;

        if (d != null) d.Damage(50, rHit.point, force);
        
        if (rHit.rigidbody != null)
        {
            rHit.rigidbody.AddForceAtPosition(force, rHit.point);
        }

        //GameObject g = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //Destroy(g, shotLifeTime);

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

    void IShootable.HideWeapon()
    {
        gameObject.SetActive(false);
    }

    void IShootable.ShowWeapon()
    {
        gameObject.SetActive(true);
    }

    bool IShootable.CanChangeWeapon()
    {
        AnimatorStateInfo aInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (aInfo.IsName("BullIdle")) return true;
        return false;
    }
}
