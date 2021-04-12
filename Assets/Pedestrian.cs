using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour, IDamageable
{
    NavMeshAgent navMeshAgent;
    float timer = 0;
    float health = 100;
    float startHealth = 0;

    [SerializeField] AudioClip sound_damage;
    [SerializeField] GameObject deadModel;

    AudioSource audioSource;

    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            Instantiate(deadModel, transform.position, transform.rotation);
            Destroy(this.gameObject);
            //Effects and stuff here
        }
        else
        {
            audioSource.PlayOneShot(sound_damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startHealth = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        MoveToSomeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            timer = 0;
            //Premade!
            MoveToSomeTarget();

            //navMeshAgent.CalculatePath();
        }

    }

    void MoveToSomeTarget()
    {
        Target[] t = FindObjectsOfType<Target>();
        navMeshAgent.SetDestination(t[Random.Range(0, t.Length)].transform.position);
    }
}

