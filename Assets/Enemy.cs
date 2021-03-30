using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private GameObject target;
    NavMeshAgent navMeshAgent;
    float timer = 0;
    float health = 300;
    float startHealth = 0;
    HealthBar hpBar;

    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        health -= dmg;
        hpBar.ChangeHealth(health / startHealth);
        if (health <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
            //Effects and stuff here
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        hpBar = GetComponent<HealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FPPlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .5f)
        {
            timer = 0;
            //Premade!
            navMeshAgent.SetDestination(target.transform.position);

            //navMeshAgent.CalculatePath();
        }
        
    }
}
