using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] float maxDistance = .4f;
    [SerializeField] int maxScore = 5;
    void IDamageable.Damage(float dmg, Vector3 position, Vector3 force)
    {
        float d = (transform.position - position).magnitude;
        if (d > maxDistance) return;
        d = d / maxDistance;
        d = 1 - d;
        d *= maxScore;
        Debug.Log((int)d+1);
    }
}
