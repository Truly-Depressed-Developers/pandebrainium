using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trash : MonoBehaviour {
    public event Action OnCollect;
    public event Action OnUncollect;

    private float zoneSize = 0;
    private float zoneX = 0, zoneY = 0;

    private bool collected = false;

    private void Update() {
        if (
            Mathf.Abs(transform.position.x - zoneX) < zoneSize / 2 &&
            Mathf.Abs(transform.position.y - zoneY) < zoneSize / 2 &&
            collected == false) {
            collected = true;
            OnCollect?.Invoke();
        } else if (
            (Mathf.Abs(transform.position.x - zoneX) >= zoneSize / 2 ||
            Mathf.Abs(transform.position.y - zoneY) >= zoneSize / 2) &&
            collected == true) {
            collected = false;
            OnUncollect?.Invoke();
        }
    }

    public void SetZoneInfo(float size, float x, float y) {
        zoneSize = size;
        zoneX = x;
        zoneY = y;
    }
}
