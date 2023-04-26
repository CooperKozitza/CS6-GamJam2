using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    Vector2 rotation = Vector2.zero;
    public float sensitivity = 1.0f;
    public bool thirdPerson;
    public ViewChanger viewChanger;

    Health healthScript;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
        healthScript = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        thirdPerson = viewChanger.thirdPerson;
        if (healthScript.playerHP != 0)
        {
            if (thirdPerson == false)
            {
                transform.position = player.transform.position + new Vector3(0, 1, 0);
                rotation.x += Input.GetAxis("Mouse X") * sensitivity;
                rotation.y = rotation.y > 90 ? 90 : rotation.y < -90 ? -90 : rotation.y + Input.GetAxis("Mouse Y") * sensitivity;
                transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up) * Quaternion.AngleAxis(rotation.y, Vector3.left);
                player.transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
            }
        } else
        {
            Destroy(player);
        }
    }
}
