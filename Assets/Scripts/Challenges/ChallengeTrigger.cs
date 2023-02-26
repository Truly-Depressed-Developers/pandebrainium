using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeTrigger : MonoBehaviour {
    public event Action OnTaskFulfilled;
    public event Action OnTaskFailure;
    public event Action<float, float> OnTimeTick;

    [SerializeField] private float maxTime = -1;
    private float currentTime;

    public float difficultyMod = 1;

    [ContextMenu("Fulfill challenge")]
    public void Fulfill() {
        OnTaskFulfilled?.Invoke();
    }

    [ContextMenu("Fail challenge")]
    public void Fail() {
        OnTaskFailure?.Invoke();
    }

    private void Start() {
        currentTime = maxTime + 0.5f;
    }

    private void Update() {
        Tick(Time.deltaTime);
    }

    private void Tick(float deltaTime) {
        if(currentTime == -1f) {
            return;
        }

        currentTime -= deltaTime;

        if(currentTime <= 0f) {
            Fail();
        }

        OnTimeTick?.Invoke(maxTime, currentTime);
    }
}
