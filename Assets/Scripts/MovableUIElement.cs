using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableUIElement : MonoBehaviour, IDragHandler {
    public void OnDrag(PointerEventData eventData) {
        transform.position = new Vector3(
            transform.position.x + eventData.delta.x,
            transform.position.y + eventData.delta.y,
            transform.position.z + 0
            );
    }
}
