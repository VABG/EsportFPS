using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform rotationPointY;
    [SerializeField] Transform rotationPointX;
    [SerializeField] Transform barrelExit;
    [SerializeField] GameObject bullet;

    [SerializeField] LayerMask viewFilter;
    [SerializeField] float viewAngle = 45;
    [SerializeField] float viewDistance = 50;
    [SerializeField] float viewForgetTime = 2.0f;
    float viewForgetTimer;

    [SerializeField] float rotationSpeedX = 50;
    [SerializeField] float rotationSpeedY = 50;
    [SerializeField] float rotationSpeedIdleY = 50;

    [SerializeField] float shotDelay;
    float shootTimer;

    GameObject target;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer > 0) shootTimer -= Time.deltaTime;

        //Rotate towards if there is a target
        if (target != null)
        {
            RotateTowardTarget();
            TryShoot();
            // Forget target if not visible for a while
            if (!CanSee(target)) {
                viewForgetTimer -= Time.deltaTime;
                if (viewForgetTimer <= 0) ForgetTarget();
            }
        }
        else //Idle behaviour (look for target)
        {
            rotationPointY.Rotate(new Vector3(0, rotationSpeedIdleY * Time.deltaTime, 0));
            rotationPointY.localRotation = 
                Quaternion.Euler(rotationPointY.localRotation.eulerAngles + new Vector3(0, rotationSpeedIdleY * Time.deltaTime, 0));
            Collider[] checkEnemies = Physics.OverlapSphere(transform.position, viewDistance, viewFilter);
            for (int i = 0; i < checkEnemies.Length; i++)
            {
                if (CanSee(checkEnemies[i].gameObject))
                {
                    target = checkEnemies[i].gameObject;
                    viewForgetTimer = viewForgetTime;
                    break;
                }
            }
        }
    }

    void ForgetTarget()
    {
        target = null;
        rotationPointX.localRotation = Quaternion.identity;
    }

    void RotateTowardTarget()
    {
        Vector3 tPos = target.transform.position;
        Vector3 selfPos = barrelExit.position;
        Vector3 direction = tPos - selfPos;        
        direction.Normalize();

        //Rotation X
        float rX = Mathf.Asin(direction.y);
        float rotX = (rX - barrelExit.forward.y) * Mathf.Rad2Deg;
        float rotateX = rotX * Time.deltaTime * rotationSpeedX;
        if (Mathf.Abs(rotateX) > Mathf.Abs(rotX)) rotateX = rotX;

        rotationPointX.Rotate(new Vector3(rotateX, 0, 0));

        //Rotation Y
        float rotY = Vector3.SignedAngle(barrelExit.forward, direction, Vector3.up);
        float rotateY = rotY * Time.deltaTime * rotationSpeedY;
        if (Mathf.Abs(rotateY) > Mathf.Abs(rotY)) rotateY = rotY;
        rotationPointY.Rotate(new Vector3(0, rotateY, 0));
    }
    void TryShoot()
    {
        if (shootTimer > 0) return;
        RaycastHit rHit;
        //Raycast to check if nothing is blocking first
        Physics.Raycast(new Ray(barrelExit.position, (target.transform.position - barrelExit.position).normalized), out rHit);

        if (rHit.collider.tag == "TeamPlayer")
        {
            Shoot();
            shootTimer = shotDelay;
        }
    }

    bool CanSee(GameObject t)
    {
        float angle = Vector3.Angle(barrelExit.forward, (t.transform.position - barrelExit.position).normalized);
        if (angle > viewAngle / 2) return false;

        RaycastHit rHit;
        if (Physics.Raycast(new Ray(barrelExit.position, (t.transform.position - barrelExit.position).normalized), out rHit)
            && rHit.transform == t.transform)
        {
            return true;
        }
        return false;
    }

    void Shoot()
    {
        audioSource.pitch = Random.value*.15f + 1;
        audioSource.Play();
        Destroy(Instantiate(bullet, barrelExit.position, barrelExit.rotation), 1);
    }    
}
