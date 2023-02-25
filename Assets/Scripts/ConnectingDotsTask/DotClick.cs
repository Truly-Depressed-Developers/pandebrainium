using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DotClick : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public UnityEvent<int> onClickEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickHandler(){
        Debug.Log("Clicked");
        onClickEvent.Invoke(int.Parse(text.text));
        Destroy(transform.parent.parent.gameObject);
    }
}
