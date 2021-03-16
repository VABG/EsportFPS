using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeWeapon : MonoBehaviour, IShootable
{
    [SerializeField] GameObject shot;
    [SerializeField] float delay = 4;
    float timer = 0;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay) canShoot = true;

    }

    public void TryShoot()
    {
        if (canShoot)
        {
            Instantiate(shot, transform.position, transform.rotation);
            timer = 0;
            canShoot = false;
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
