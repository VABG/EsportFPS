using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface IHealthBar
//{
//    abstract float GetHealthPercentage();
//}

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject hpRepresentation;
    float health;
    float hpBarScale;
    //[SerializeField] IHealthBar healthObj;

    Transform cam;

    public void ChangeHealth(float health)
    {
        this.health = health;
        hpRepresentation.transform.localScale = new Vector3(health * hpBarScale, hpRepresentation.transform.localScale.y, hpRepresentation.transform.localScale.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        hpBarScale = hpRepresentation.transform.localScale.x;
        cam = FindObjectOfType<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.LookAt(cam);
    }
}
