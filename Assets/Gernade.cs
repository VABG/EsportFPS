using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gernade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float delay = 3;
    [SerializeField] float throwForce = 10;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            Destroy(Instantiate(explosion, transform.position, transform.rotation), 10);
            Destroy(gameObject);
        }
    }
}
