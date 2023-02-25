using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour {
    [SerializeField] RectTransform timerForeground;
    [SerializeField] RectTransform timerBackground;

    public event Action OnTaskFulfilled;
    public event Action OnTaskFailure;

    public void ChallengeFulfilled() {
        OnTaskFulfilled?.Invoke();
        Destroy(this.gameObject);
    }

    public void ChallengeFailed() {
        OnTaskFailure?.Invoke();
        Destroy(this.gameObject);
    }

    public void SetTimer(float maxTime, float currentTime) {
        timerForeground.sizeDelta = new Vector2(
            (currentTime / maxTime) * timerBackground.rect.width,
            timerForeground.sizeDelta.y
            );
    }
}
