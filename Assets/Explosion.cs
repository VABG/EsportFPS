using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5;
    [SerializeField] float explosionForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < c.Length; i++)
        {
            Rigidbody rb = c[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
