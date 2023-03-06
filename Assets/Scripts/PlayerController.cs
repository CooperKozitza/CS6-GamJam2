using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 mousePos;
    public float rotationSpeed = 1;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(Input.GetAxisRaw("Horizontal") * speed, 0.0F, Input.GetAxisRaw("Vertical") * speed);
    }
}
