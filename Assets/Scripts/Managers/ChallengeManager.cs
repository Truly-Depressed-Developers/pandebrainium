using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour {

    [SerializeField] List<GameObject> challengeList;
    [SerializeField] Transform challengeContainer;

    [ContextMenu("Spawn Random Task")]
    void SpawnTask() {
        if (challengeList == null || challengeList.Count == 0) {
            Debug.LogWarning("Tasklist is empty");
            return;
        }

        // Pick random challenge from list
        int randomIndex = Random.Range(0, challengeList.Count);
        GameObject randomChallenge = challengeList[randomIndex];

        Debug.Log("Spawned task: " + randomChallenge.ToString());

        // Try to find Challenge component on root
        bool componentExists = randomChallenge.TryGetComponent<Challenge>(out Challenge _);

        if (componentExists == false) {
            Debug.LogWarning("Task " + randomChallenge.ToString() + " does not have Task component in root");
            return;
        }

        // Spawn challenge and grab components
        GameObject spawnedChallenge = Instantiate(randomChallenge, challengeContainer);
        Transform challengePanel = spawnedChallenge.transform.GetChild(0);

        RectTransform challengeRect = challengePanel.GetComponent<RectTransform>();

        // Calculate spawn position
        Vector2 challengePanelSize = challengeRect.sizeDelta;

        // TODO: Get the available spawn range from somewhere
        Vector2 maxSpawnRange = new(1920, 1080);

        Vector2 spawnRange = maxSpawnRange - challengePanelSize;
        int randomX = (int) (Random.Range(0, spawnRange.x) - spawnRange.x / 2);
        int randomY = (int) (Random.Range(0, spawnRange.y) - spawnRange.y / 2);

        Debug.Log(randomX + " - " + randomY);

        // Move the challenge panel
        challengeRect.localPosition = new Vector3(randomX, randomY, challengeRect.localPosition.z); 


        // Register events
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
