using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour {
    [SerializeField] private TMP_Text textElement;

    public void SetText(int day) {
        textElement.text = $"Day {day}";
    }
}
