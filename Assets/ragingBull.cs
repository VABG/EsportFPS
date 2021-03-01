using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragingBull : MonoBehaviour
{
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
    [SerializeField] float shotLifeTime = 5.0f;
    [SerializeField] Light shotLight;
    [SerializeField] float shotLightLifeTime = .05f;
    private float shotLightLifeCounter = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        shotLightLifeCounter += Time.deltaTime;
        if (shotLightLifeCounter > shotLightLifeTime) shotLight.enabled = false;
    }



    public void ShootBullet()
    {
        GameObject g = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Destroy(g, shotLifeTime);
        shotLight.enabled = true;
        shotLightLifeCounter = 0;
    }
}
