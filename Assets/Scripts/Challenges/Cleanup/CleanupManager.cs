using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CleanupManager : MonoBehaviour {

    [SerializeField] private int baseTrash = 4;

    [SerializeField] GameObject trashPrefab1;
    [SerializeField] GameObject trashPrefab2;
    [SerializeField] ChallengeTrigger trigger;
    [SerializeField] RectTransform zone;
    [SerializeField] RectTransform panel;

    private List<GameObject> trashPrefabs;
    private int trashModelIndex;
    private int collectedTrash;
    private int totalTrash;

    private List<Trash> trashList;

    // Start is called before the first frame update
    void Start() {
        trashPrefabs = new List<GameObject>() { trashPrefab1, trashPrefab2 };
        trashModelIndex = Random.Range(0, 2);
        GameObject currentTrashPrefab = trashPrefabs[trashModelIndex];

        trashList = new List<Trash>();

        // Position stuff
        float trashSize = currentTrashPrefab.GetComponent<RectTransform>().rect.width;
        Vector2 panelSize = panel.sizeDelta;

        collectedTrash = 0;
        totalTrash = Mathf.FloorToInt(baseTrash * Mathf.Pow(trigger.difficultyMod, 1.2f));

        float zoneSize = zone.rect.width;
        float zoneX = zone.position.x;
        float zoneY = zone.position.y;

        for(int i = 0; i < totalTrash; i++) {
            GameObject trashObject = Instantiate(currentTrashPrefab, transform.parent);
            Trash trashComponent = trashObject.GetComponent<Trash>();
            trashList.Add(trashComponent);

            trashComponent.SetZoneInfo(zoneSize, zoneX, zoneY);

            trashComponent.OnCollect += OnTrashCollected;
            trashComponent.OnUncollect += OnTrashUncollected;

            // Random position
            Vector2 randomPos = new Vector2(
                Random.Range(0, panelSize.x - trashSize) - (panelSize.x - trashSize) / 2,
                Random.Range(0, panelSize.y - trashSize) - (panelSize.y - trashSize) / 2
                );

            trashObject.transform.localPosition = randomPos;


            // Random rotation
            Vector3 angles = trashObject.transform.eulerAngles;
            trashObject.transform.eulerAngles = new Vector3(angles.x, angles.y, Random.Range(0f, 360f));
        }
    }

    void Update() {
        if(collectedTrash == totalTrash) {
            trigger.Fulfill();
        }

        float zoneSize = zone.rect.width;
        float zoneX = zone.position.x;
        float zoneY = zone.position.y;

        foreach(Trash t in trashList) {
            t.SetZoneInfo(zoneSize, zoneX, zoneY);
        }
    }

    void OnTrashCollected() {
        ++collectedTrash;
    }

    void OnTrashUncollected() {
        --collectedTrash;
    }
}
