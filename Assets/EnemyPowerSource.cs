using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerSource : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject powering;
    [SerializeField] float health = 50;
    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(powering);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
