using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] float health = 20;
    bool used = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (used) return;
        FPPlayerController fp = collision.collider.GetComponent<FPPlayerController>();
        if (fp != null)
        {
            if (fp.TryAddHealth(health))
            {
                used = true;
                Destroy(this.gameObject);
            }
        }
    }
}
