using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBtn : MonoBehaviour
{
    private Color activColor = new Color(0.5f, 0.5f, 1, 0.6f);
    private Color noActivColor = new Color(1, 1, 1, 0);

    public event Action<int, bool> OnClickToParent;

    private bool active = false;

    private int posId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPosId(int val)
    {
        posId = val;
    }

    public void onClick()
    {
        Debug.Log("CLICK");

        active = !active;

        GetComponent<Image>().color = active ? activColor : noActivColor;
        OnClickToParent?.Invoke(posId, active);
    }
}
