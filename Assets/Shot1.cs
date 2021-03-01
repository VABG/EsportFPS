using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot1 : MonoBehaviour
{
    [SerializeField] float shotSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = transform.forward * shotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
