using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour {

    static Vector2 CalculateTotalPanelSize(Vector2 innerSize) {
        return innerSize + new Vector2(10 + 10, 10 + 10 + 20 + 10);
    }

    [SerializeField] GameObject challengeBasePrefab;
    [SerializeField] List<GameObject> challengePrefabList;
    [SerializeField] Transform challengeContainer;

    [ContextMenu("Spawn Random Task")]
    void SpawnTask() {
        if (challengePrefabList == null || challengePrefabList.Count == 0) {
            Debug.LogWarning("Tasklist is empty");
            return;
        }

        // Pick random challenge from list
        int randomIndex = Random.Range(0, challengePrefabList.Count);
        GameObject randomChallenge = challengePrefabList[randomIndex];

        Debug.Log("Spawned task: " + randomChallenge.ToString());


        // Spawn challenge and grab components
        GameObject challenge = Instantiate(randomChallenge);
        Transform challengePanel = challenge.transform.GetChild(0);

        RectTransform challengeRect = challengePanel.GetComponent<RectTransform>();


        // Spawn challengeBase and embed the selected challenge
        GameObject challengeBase = Instantiate(challengeBasePrefab, challengeContainer);
        Challenge challengeBaseChallengeComponent = challengeBase.GetComponent<Challenge>();
        Transform challengeBasePanel = challengeBase.transform.GetChild(0);
        RectTransform challengeBasePanelRect = challengeBasePanel.GetComponent<RectTransform>();


        // Register events
        ChallengeTrigger challengeObjectChallengeTrigger = challengePanel.GetComponent<ChallengeTrigger>();

        challengeObjectChallengeTrigger.OnTaskFulfilled += challengeBaseChallengeComponent.ChallengeFulfilled;
        challengeObjectChallengeTrigger.OnTaskFailure += challengeBaseChallengeComponent.ChallengeFailed;

        challengeBaseChallengeComponent.OnTaskFulfilled += OnTaskFulfilled;
        challengeBaseChallengeComponent.OnTaskFailure += OnTaskFailure;

        challengeObjectChallengeTrigger.OnTimeTick += challengeBaseChallengeComponent.SetTimer;


        // Calculate and set challengeBase dimensions
        Vector2 challengeBaseSize = ChallengeManager.CalculateTotalPanelSize(challengeRect.sizeDelta);
        challengeBasePanel.GetComponent<RectTransform>().sizeDelta = challengeBaseSize;


        // Copy challenge prefabs to display
        Transform challengeBaseDisplay = challengeBase.transform.GetChild(0).GetChild(0);
        challengePanel.SetParent(challengeBaseDisplay);
        challengePanel.GetComponent<RectTransform>().localPosition = Vector3.zero;

        // Calculate spawn position
        Vector2 challengePanelSize = challengeBaseSize;

        // TODO: Get the available spawn range from somewhere
        Vector2 maxSpawnRange = new(1920, 1080);

        Vector2 spawnRange = maxSpawnRange - challengePanelSize;
        int randomX = (int)(Random.Range(0, spawnRange.x) - spawnRange.x / 2);
        int randomY = (int)(Random.Range(0, spawnRange.y) - spawnRange.y / 2);

        Debug.Log(randomX + " - " + randomY);

        // Move the challenge panel
        challengeBasePanelRect.localPosition = new Vector3(randomX, randomY, challengeBasePanelRect.localPosition.z);
    }

    [ContextMenu("Spawn 20 challenges")]
    void SpawnMany() {
        for (int i = 0; i < 20; i++) {
            this.SpawnTask();
        }
    }

    void OnTaskFulfilled() {
        Debug.Log("Task fulfilled.");
    }

    void OnTaskFailure() {
        Debug.Log("Task failed successfully.");
    }
}
