using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour {
    private TMP_Text textElement;

    private void Start() {
        textElement = GetComponent<TMP_Text>();
    }

    public void SetText(int day) {
        textElement.text = $"Day {day}";
    }
}
