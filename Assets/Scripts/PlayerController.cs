using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 mousePos;
    public float rotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0.0F, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
    }
}
