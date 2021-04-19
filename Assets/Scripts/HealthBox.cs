using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] float health = 20;

    private void OnCollisionEnter(Collision collision)
    {
        FPPlayerController fp = collision.collider.GetComponent<FPPlayerController>();
        if (fp != null)
        {
            if (fp.TryAddHealth(health))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
