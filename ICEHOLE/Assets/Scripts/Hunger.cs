using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    
    
    public GameObject hi;
    public Hook hookScript;
    public int hunger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatFish() 
    {
        hookScript.fishWasHooked = false;

    }
}