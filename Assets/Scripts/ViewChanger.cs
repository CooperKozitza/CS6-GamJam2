using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ViewChanger : MonoBehaviour
{
    public GameObject _1stPersonCam;
    private CinemachineBrain _1stCam;
    public bool thirdPerson = false;

    void Start()
    {
        _1stCam = _1stPersonCam.GetComponent<CinemachineBrain>();


    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (thirdPerson == true)
            {
                thirdPerson = false;
                _1stCam.enabled = false;
            }
            else
            {
                thirdPerson = true;
                _1stCam.enabled = true;

            }
        }
    }
}