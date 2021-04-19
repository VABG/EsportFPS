using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerSource : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject powering;
    IDamageable damagable;
    [SerializeField] float health = 50;
    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        health -= dmg;
        if (health <= 0 && powering != null) 
        {
            damagable.Damage(1000, Vector3.zero, Vector3.up * 50);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        damagable = powering.GetComponent<IDamageable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
