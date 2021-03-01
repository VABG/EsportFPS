using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragingBull : MonoBehaviour
{
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
    [SerializeField] float shotLifeTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void ShootBullet()
    {
        GameObject g = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Destroy(g, shotLifeTime);
    }
}
