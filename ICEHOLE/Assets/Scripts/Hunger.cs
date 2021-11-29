using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;



public class Hunger : MonoBehaviour
{
    
    private UnityEngine.UI.Slider hungerSlider;
    //private GameObject eatFishButton;
    
    
    public Hook hookScript;
   // public int hunger;
    // Start is called before the first frame update
    void Start()
    {
        //eatFishButton = GameObject.Find("EatFishButton");
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
        hungerSlider.value += 5;
        Destroy(hookScript.fishes);
        hookScript.eatFishButton.SetActive(false);

        Debug.Log("Is fish hooked: " + hookScript.fishWasHooked);
    }
}
