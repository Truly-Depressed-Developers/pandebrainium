using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{
    [SerializeField] TMP_Text txt;

    private float dayStart;
    private float dayEnd;
    private float dayTime;

    private int dayInSec = 24 * 60 * 60;
    // Start is called before the first frame update
    void Start()
    {
        dayTime = ChallengeManager.instance.dayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (dayStart != LoopManager.instance.dayStart) setDayStartEnd();

        txt.SetText(calculateHour(Time.time - dayStart));
    }

    string calculateHour(float pastTime)
    {
        float percent = pastTime / dayTime;

        int workTime = dayInSec - 8 * 60 * 60;

        float pastWorkTime = percent * workTime;

        int hours = 8 + (Mathf.FloorToInt(pastWorkTime / (60 * 60)) % 24);
        int mins = Mathf.FloorToInt((pastWorkTime - ((hours - 8) * 60 * 60)) / 60) % 60;

        return (hours < 10 ? "0" : "") + hours + ":" + (mins < 10 ? "0" : "") + mins;
    }

    void setDayStartEnd()
    {
        dayStart = LoopManager.instance.dayStart;
        dayEnd = dayStart + Mathf.FloorToInt(dayTime);
    }
}
