using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        credits = GameObject.Find("Credits");
        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeCreditsVisible() 
    {
        if (credits.activeInHierarchy)
        {
            credits.SetActive(false);
        } else {
            credits.SetActive(true);
        }
        
    }
}
