using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerController : MonoBehaviour
{
    [SerializeField] float accelerationGround = 5;
    [SerializeField] float accelerationAir = 5;
    [SerializeField] float jumpVelocity = 5;
    [SerializeField] float dragGround = 8;
    [SerializeField] float dragAir = .1f;

    //Animation
    [SerializeField] ragingBull gun;


    Rigidbody rb;
    Vector3 lastMoveInput = Vector3.zero;
    Vector3 fwd;
    Camera cam;
    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        // Hide mouse
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        MovementInput();
        ViewInput();
        JumpInput();
        ShootInput();
        if (onGround)
        {
            rb.drag = dragGround;
        }
        else
        {
            rb.drag = dragAir;
        }
    }

    void JumpInput()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up*jumpVelocity, ForceMode.VelocityChange);
    }

    void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            gun.LookAtGun();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Reload();
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
}
