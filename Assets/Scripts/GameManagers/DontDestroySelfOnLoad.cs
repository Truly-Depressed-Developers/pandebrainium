using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySelfOnLoad : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
