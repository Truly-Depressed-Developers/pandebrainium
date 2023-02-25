using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupManager : MonoBehaviour {

    [SerializeField] private int baseTrash = 4;

    [SerializeField] GameObject trashPrefab;
    [SerializeField] ChallengeTrigger trigger;
    [SerializeField] RectTransform zone;
    [SerializeField] RectTransform panel;

    private int collectedTrash;
    private int totalTrash;

    // Start is called before the first frame update
    void Start() {
        // Position stuff
        float trashSize = trashPrefab.GetComponent<RectTransform>().rect.width;
        Vector2 panelSize = panel.sizeDelta;


        collectedTrash = 0;
        totalTrash = baseTrash; // * jakieœ zmienne

        float zoneSize = zone.rect.width;
        float zoneX = zone.position.x;
        float zoneY = zone.position.y;

        for(int i = 0; i < totalTrash; i++) {
            GameObject trashObject = Instantiate(trashPrefab, transform.parent);
            Trash trashComponent = trashObject.GetComponent<Trash>();

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
    }

    void OnTrashCollected() {
        ++collectedTrash;
    }

    void OnTrashUncollected() {
        --collectedTrash;
    }
}
