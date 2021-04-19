using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int grenades;
    [SerializeField] int bullets = 6;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        FPPlayerController fp = collision.collider.GetComponent<FPPlayerController>();
        if (fp != null)
        {
            if (fp.TryAddAmmo(bullets, grenades))
            {
                Destroy(this.gameObject);
            }
        }
    }

}
