using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    FPPlayerController p;
    // Start is called before the first frame update
    void Start()
    {
       p = GetComponentInParent<FPPlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        p.SetOnGround(true);
    }

}
