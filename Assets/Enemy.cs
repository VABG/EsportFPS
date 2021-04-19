using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private GameObject target;
    NavMeshAgent navMeshAgent;
    float timer = 0;
    float health = 100;
    float startHealth = 0;
    HealthBar hpBar;

    [SerializeField] AudioClip growl;
    [SerializeField] GameObject deadModel;

    AudioSource audioSource;
    float grrDelayTimer = 1.5f;

    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        health -= dmg;
        hpBar.ChangeHealth(health / startHealth);
        if (health <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
            //Effects and stuff here
            Instantiate(deadModel, transform.position, transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grrDelayTimer = Random.Range(3, 10.0f);
        audioSource = GetComponent<AudioSource>();
        startHealth = health;
        hpBar = GetComponent<HealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FPPlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        grrDelayTimer -= Time.deltaTime;
        if (grrDelayTimer <= 0)
        {
            grrDelayTimer = Random.Range(2, 6.0f);
            audioSource.PlayOneShot(growl);
        }

        timer += Time.deltaTime;
        if (timer > 2.5f)
        {
            timer = 0;
            //Premade!
            Vector2 rndPos = Random.insideUnitCircle * 5;
            navMeshAgent.SetDestination(new Vector3(transform.position.x + rndPos.x,  transform.position.x + rndPos.y, transform.position.z));

            //navMeshAgent.CalculatePath();
        }
        
    }
}
