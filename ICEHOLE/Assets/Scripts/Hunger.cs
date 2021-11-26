using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;



public class Hunger : MonoBehaviour
{
    
    public UnityEngine.UI.Slider hungerSlider;

    public GameObject hungerSlider2;
    public GameObject hi;
    public Hook hookScript;
    public int hunger;
    // Start is called before the first frame update
    void Start()
    {

        hungerSlider = GameObject.Find("Hunger Slider").GetComponent<UnityEngine.UI.Slider>();
        hungerSlider.value = 10;
        StartCoroutine("HungerRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HungerRoutine() 
    {
        yield return new WaitForSeconds(8);
        hungerSlider.value--;
    }

    public void EatFish() 
    {
        hookScript.fishWasHooked = false;
        hungerSlider.value += 2;
    }
}
