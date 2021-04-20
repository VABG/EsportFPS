using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 600;
    float maxHealth;
    bool alive = true;

    [SerializeField] float accelerationGround = 5;
    [SerializeField] float accelerationAir = 5;
    [SerializeField] float jumpVelocity = 5;
    [SerializeField] float dragGround = 8;
    [SerializeField] float dragAir = .1f;
    [SerializeField] AudioClip soundGotHealth;
    [SerializeField] AudioClip soundGotAmmo;
    [SerializeField] AudioClip soundJumped;

    [SerializeField] GameObject hpBar;
    [SerializeField] PlayerDamageUI damageUI;

    [SerializeField] List<GameObject> weapons;
    List<IShootable> guns;
    int currentWeapon = 0;
    AudioSource audioSource;

    Rigidbody rb;
    Vector3 lastMoveInput = Vector3.zero;
    Vector3 fwd;
    Camera cam;
    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (health <= 0)
        {
            health = 1;
            Debug.LogError("Health set to 0 or less!");
        }

        maxHealth = health;
        guns = new List<IShootable>();

        for (int i = 0; i < weapons.Count; i++)
        {
            IShootable g = weapons[i].GetComponent<IShootable>();
            if (g != null)
            {
                guns.Add(g);
            }
            if (i != currentWeapon) g.HideWeapon();
        }
        // Get Rigidbody
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        // Hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            // Input
            //rb.angularVelocity = Vector3.zero;
            MovementInput();
            ViewInput();
            JumpInput();
            ShootInput();
            ChangeWeaponInput();
            if (onGround)
            {
                rb.drag = dragGround;
            }
            else
            {
                rb.drag = dragAir;
            }
        }

    }

    void JumpInput()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {            
            rb.AddForce(Vector3.up * (jumpVelocity - rb.velocity.y), ForceMode.VelocityChange);
            audioSource.pitch = Random.value * .1f + .8f;
            audioSource.PlayOneShot(soundJumped);
        }
    }

    void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            guns[currentWeapon].TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            guns[currentWeapon].LookAtWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            guns[currentWeapon].Reload();
        }
    }

    void ChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!guns[currentWeapon].CanChangeWeapon() || currentWeapon == 0) return;
            guns[currentWeapon].HideWeapon();
            currentWeapon = 0;
            guns[currentWeapon].ShowWeapon();         
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!guns[currentWeapon].CanChangeWeapon() || currentWeapon == 1) return;
            guns[currentWeapon].HideWeapon();
            currentWeapon = 1;
            guns[currentWeapon].ShowWeapon();
        }
    }

    void MovementInput()
    {
        // Reset Input
        lastMoveInput = Vector3.zero;
        // Get Input
        if (Input.GetKey(KeyCode.W)) lastMoveInput += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) lastMoveInput += Vector3.back;
        if (Input.GetKey(KeyCode.D)) lastMoveInput += Vector3.right;
        if (Input.GetKey(KeyCode.A)) lastMoveInput += Vector3.left;

        //  Normalize (length = 1)
        lastMoveInput.Normalize();
    }

    void ViewInput()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 rotation = cam.transform.rotation.eulerAngles;
        cam.transform.rotation = Quaternion.Euler(rotation.x - mouseInput.y, rotation.y + mouseInput.x, rotation.z);

        fwd = cam.transform.forward;
        fwd = new Vector3(fwd.x, 0, fwd.z);
        fwd.Normalize();
    }

    public void SetOnGround(bool onGround)
    {
        this.onGround = onGround;
    }

    private void FixedUpdate()
    {
        float currentAcceleration = onGround ? accelerationGround : accelerationAir;
        rb.AddForce(fwd * lastMoveInput.z * currentAcceleration, ForceMode.Acceleration);
        rb.AddForce(cam.transform.right * lastMoveInput.x * currentAcceleration, ForceMode.Acceleration);
        SetOnGround(false);
    }

    public void Damage(float dmg, Vector3 position, Vector3 force)
    {
        health -= dmg;
        damageUI.Damage(dmg/health * 2);
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        hpBar.transform.localScale = new Vector3(health / maxHealth, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }

    public bool TryAddAmmo(int bullets, int grenades)
    {
        bool gotAmmo = false;
        if (guns[0].TryAddAmmo(bullets)) gotAmmo = true;
        if (guns[1].TryAddAmmo(grenades)) gotAmmo = true;
        audioSource.pitch = Random.value * .1f + .95f; 
        if (gotAmmo) audioSource.PlayOneShot(soundGotAmmo);

        return gotAmmo;
    }

    public bool TryAddHealth(float healthPlus)
    {
        if (health < maxHealth)
        {
            health += healthPlus;
            if (health > maxHealth) health = maxHealth;
            UpdateHealthBar();
            audioSource.pitch = Random.value * .1f + .95f;
            audioSource.PlayOneShot(soundGotHealth);
            return true;
        }
        else return false;
    }

    private void Die()
    {
        damageUI.Die();
        alive = false;
        rb.freezeRotation = false;
        rb.drag = 10;
        rb.angularDrag = 10f;
        rb.angularVelocity -= cam.transform.forward * 50;
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].HideWeapon();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager gmgr = FindObjectOfType<GameManager>();
        if (gmgr != null) gmgr.PlayerDied();
        else { Debug.LogError("No game manager found!"); }
    }

    public void Disable()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].HideWeapon();
        }
        alive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        rb.drag = 1000;
    }
}
