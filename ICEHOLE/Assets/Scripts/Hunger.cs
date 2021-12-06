using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Hunger : MonoBehaviour
{
    
    private UnityEngine.UI.Slider hungerSlider;
    //private GameObject eatFishButton;
    private int hungerTime;
    private GameObject youStarvedText;
    
    public Hook hookScript;
   // public int hunger;
    // Start is called before the first frame update
    void Start()
    {
        //eatFishButton = GameObject.Find("EatFishButton");
        hungerSlider = GameObject.Find("Hunger Slider").GetComponent<UnityEngine.UI.Slider>();
        youStarvedText = GameObject.Find("You Starved");
        youStarvedText.SetActive(false);
        hungerTime = Random.Range(5, 16);
        hungerSlider.value = 10;
        StartCoroutine("HungerRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HungerRoutine() 
    {
        yield return new WaitForSeconds(hungerTime);
        hungerSlider.value--;
        hungerTime = Random.Range(5, 16);
        if (hungerSlider.value <= 0) 
        {
            youStarvedText.SetActive(true);
            
        }
        StartCoroutine("HungerRoutine");
    }

    public void EatFish() 
    {
        hookScript.fishWasHooked = false;
        hungerSlider.value += 5;
        Destroy(hookScript.fishes);
        hookScript.eatFishButton.SetActive(false);

        Debug.Log("Is fish hooked: " + hookScript.fishWasHooked);
    }

    IEnumerator GameOverRoutine() 
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("FishingHole");
    }
}
