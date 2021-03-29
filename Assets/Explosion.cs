using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5;
    [SerializeField] float explosionForce = 100;
    [SerializeField] float damage = 500;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < c.Length; i++)
        {
            IDamageable dmg = c[i].GetComponent<IDamageable>();
            Vector3 pos = c[i].ClosestPoint(transform.position);
            Vector3 direction = c[i].transform.position - transform.position;
            // Get distance to
            float d = (direction).magnitude;
            // Divide by max distance
            d /= explosionRadius;
            // Invert
            d = 1 - d;


            if (dmg != null)
            {
                dmg.Damage(damage*d, pos, direction);
            }
            Rigidbody rb = c[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Change curve
                d *= d * d * d;
                // Drag
                float drag = rb.drag;
                // Approximate how much area is hit by blast
                float sizeDrag = c[i].bounds.size.magnitude;
                // Limit scale
                if (sizeDrag > 2) sizeDrag = 2;
                if (sizeDrag < .02) sizeDrag = .02f;                
                // Limit drag
                if (drag < .02f) drag = .02f;
                else if (drag > 1.0f) drag = 1.0f;
                // Comine drag
                drag = (drag + sizeDrag);
                // Add force
                rb.AddForceAtPosition(direction.normalized * explosionForce * d * drag, pos);
                //rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
