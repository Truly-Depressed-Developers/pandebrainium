using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour {

    [SerializeField] List<GameObject> challengeList;
    [SerializeField] Transform challengeContainer;

    [ContextMenu("Spawn Random Task")]
    void SpawnTask() {
        if(challengeList == null || challengeList.Count == 0) {
            Debug.LogWarning("Tasklist is empty");
            return;
        }

        int randomIndex = Random.Range(0, challengeList.Count);
        GameObject randomChallenge = challengeList[randomIndex];

        Debug.Log("Spawned task: " + randomChallenge.ToString());

        // Try to find Task component on root
        bool componentExists = randomChallenge.TryGetComponent<Challenge>(out Challenge _);

        if (componentExists == false) {
            Debug.LogWarning("Task " + randomChallenge.ToString() + " does not have Task component in root");
            return;
        }

        GameObject spawnedChallenge = Instantiate(randomChallenge,challengeContainer);

        Challenge challengeComponent = spawnedChallenge.GetComponent<Challenge>();

        challengeComponent.OnTaskFulfilled += OnTaskFulfilled;
        challengeComponent.OnTaskFailure += OnTaskFailure;
    }

    void OnTaskFulfilled() {
        Debug.Log("Task fulfilled.");
    }

    void OnTaskFailure() {
        Debug.Log("Task failed successfully.");
    }
}
