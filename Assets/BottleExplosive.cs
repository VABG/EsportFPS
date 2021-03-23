using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    abstract public void Damage(float dmg, Vector3 position, Vector3 force);
}

public class BottleExplosive : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject explosion;
    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        Destroy(gameObject);
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
    }
}
