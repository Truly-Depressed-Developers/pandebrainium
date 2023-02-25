using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour {
    [SerializeField] RectTransform timerForeground;
    [SerializeField] RectTransform timerBackground;

    public event Action OnTaskFulfilled;
    public event Action OnTaskFailure;

    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void ChallengeFulfilled() {
        OnTaskFulfilled?.Invoke();
        Finish();
    }

    public void ChallengeFailed() {
        OnTaskFailure?.Invoke();
        Finish();
    }

    private void Finish() {
        animator.SetTrigger("Finish");
        Destroy(this.gameObject, 0.2f);
    }

    public void SetTimer(float maxTime, float currentTime) {
        Debug.Log(currentTime + " | " + maxTime + " | " + timerBackground.sizeDelta.x);
        timerForeground.sizeDelta = new Vector2(
            (currentTime / maxTime) * timerBackground.rect.width,
            timerForeground.sizeDelta.y
            );
    }
}
