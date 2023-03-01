using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{

    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startingHour;

    [SerializeField]
    private TextMeshProUGUI timeDisplay;

    private DateTime currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startingHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeDisplay != null)
        {
            timeDisplay.text = currentTime.ToString("HH:mm");
        }
    }
}
