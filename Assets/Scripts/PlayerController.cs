using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 mousePos;
    public float rotationSpeed = 1;
    public Rigidbody rb;
    public bool onGround;
    public float jumpForce = 250;
    public float jumpCooldown = 1;
    bool readyToJump = true;
    public GameObject camOrient;
    public GameObject playerLookAt;
    public ViewChanger viewChanger;
    bool thirdPerson = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        thirdPerson = viewChanger.thirdPerson;
        if (thirdPerson)
        {
            Vector3 movement = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed);
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.cyan);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
            this.transform.LookAt(new Vector3(camOrient.transform.position.x, this.transform.position.y, camOrient.transform.position.z));
            this.transform.Rotate(0, 180, 0);
        }
        else
        {
            Vector3 movement = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
        if (Physics.Raycast(transform.position, Vector3.down, 1F))
        {
            onGround = true;
            Debug.DrawRay(transform.position, Vector3.down, Color.yellow);
        }
        else
        {
            onGround = false;
        }

        if (Input.GetButton("Jump") && onGround && readyToJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        rb.AddRelativeForce(Input.GetAxisRaw("Horizontal") * speed, 0.0F, Input.GetAxisRaw("Vertical") * speed);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
