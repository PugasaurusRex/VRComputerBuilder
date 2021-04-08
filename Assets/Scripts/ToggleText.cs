using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleText : MonoBehaviour
{
    public bool isVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle(GameObject text)
    {
        if(isVisible)
        {
            isVisible = false;
            text.gameObject.SetActive(false);
        }
        else
        {
            isVisible = true;
            text.gameObject.SetActive(true);
        }
    }
}
