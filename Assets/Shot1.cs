using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot1 : MonoBehaviour
{
    [SerializeField] GameObject hitSpawn;
    [SerializeField] float shotSpeed = 10;
    [SerializeField] float damage = 50;
    Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.velocity = transform.forward * shotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable dmg = collision.collider.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.Damage(damage, collision.contacts[0].point, r.velocity);
        }
        Destroy(Instantiate(hitSpawn), 1);
        Destroy(this.gameObject);
    }
}
