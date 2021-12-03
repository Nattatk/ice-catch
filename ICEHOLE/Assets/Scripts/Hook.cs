using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hook : MonoBehaviour
{
    private GameObject pole;
    private PlayQuickSound playQuickSound;
    public GameObject fishes;
    
    public GameObject eatFishButton;
    LineRenderer lineRenderer;

    bool isHooking;
    public bool fishWasHooked;

    float hookDistance;
    Vector3 originalPosition;

    Rigidbody rigid;
   

    // Start is called before the first frame update
   public void Awake()
    {
        playQuickSound = GetComponent<PlayQuickSound>();
        pole = GameObject.FindGameObjectWithTag("Pole");

        eatFishButton = GameObject.Find("EatFishButton");
        eatFishButton.SetActive(false);

        lineRenderer = GetComponent<LineRenderer>();
        isHooking = false;
        fishWasHooked = false;
        hookDistance = 0;
        rigid = GetComponent<Rigidbody>();
        originalPosition = new Vector3(pole.transform.position.x, pole.transform.position.y, pole.transform.position.z); 
    }

    // Update is called once per frame
    void Update()
    {
        originalPosition = new Vector3(pole.transform.position.x, pole.transform.position.y, pole.transform.position.z);
        lineRenderer.SetPosition(0, originalPosition);
        lineRenderer.SetPosition(1, transform.position);

        if (Input.GetMouseButtonDown(0) && !isHooking && !fishWasHooked)
        {
            StartHooking();
        }
        ReturnHook();
        CatchFish();

    }

    private void StartHooking()
    {
        isHooking = true;
        rigid.isKinematic = false;
        rigid.AddForce(-transform.up * Constants.HOOK_SPEED);
    }

    private void ReturnHook()
    {
        if (isHooking)
        {
            hookDistance = Vector3.Distance(transform.position, originalPosition);
            if (hookDistance > Constants.MAX_HOOK_DISTANCE)
            {
                rigid.isKinematic = true;
                transform.position = originalPosition;
                isHooking = false;
            }
        }
    }

    private void CatchFish()
    {
        if (fishWasHooked)
        {
            Vector3 finalPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            fishes.transform.position = Vector3.MoveTowards(fishes.transform.position, finalPosition, Constants.MAX_HOOK_DISTANCE);
            eatFishButton.SetActive(true);
            
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Fishes"))
        {
            Debug.Log("Hooked");
            fishWasHooked = true;
            fishes = collider.gameObject;
            playQuickSound.Play();
        }
    }

    
}
