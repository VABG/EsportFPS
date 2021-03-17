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
                // Get distance to
                Vector3 direction = rb.transform.position - transform.position;
                float d = (direction).magnitude;
                // Divide by max distance
                d /= explosionRadius;
                // Invert
                d = 1 - d;
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
                rb.AddForceAtPosition(direction.normalized * explosionForce * d * drag, c[i].ClosestPoint(transform.position));
                //rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
