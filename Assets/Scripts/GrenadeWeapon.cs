using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeWeapon : MonoBehaviour, IShootable
{
    [SerializeField] GameObject shot;
    [SerializeField] float delay = 4;
    [SerializeField] int maxAmmo = 6;
    [SerializeField] WeaponUI weaponUI;

    float timer = 0;
    bool canShoot = true;
    int ammo = 3;

    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
        weaponUI.SetGrenadesUI(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay) canShoot = true;
    }

    public void TryShoot()
    {
        if (canShoot && ammo > 0)
        {
            Instantiate(shot, transform.position, transform.rotation);
            timer = 0;
            ammo--;
            canShoot = false;
            weaponUI.SetGrenadesUI(ammo);
        }
    }

    public void LookAtWeapon()
    {

    }

    public void Reload()
    {

    }

    public void ShowWeapon()
    {
        gameObject.SetActive(true);
    }

    public void HideWeapon()
    {
        gameObject.SetActive(false);
    }

    public bool CanChangeWeapon()
    {
        return true;
    }
}
