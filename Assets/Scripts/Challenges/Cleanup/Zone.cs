using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    public float size { get; private set; }

    void Start() {
        size = GetComponent<RectTransform>().rect.width;
    }
}
