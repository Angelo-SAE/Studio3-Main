using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    int numberTimes = 0;
    int numberTimes2 = 0;

    public GameObject thePlayer;
    public GameObject grandfather;
    public GameObject customerCounter;

    public GameObject mainCamera;
    public GameObject tutorialCamera;
    public GameObject kitchenCamera;

    public GameObject playerUI;
    public GameObject notesUI;
    public GameObject blackScreen;
    public GameObject leaveTutorial;
    public GameObject startBasics;
    public GameObject startCooking;
    public GameObject teleporterEnd;

    public PlayableDirector stage2;
    public InteractEvent firstInteract;
    public FeedGrandfather secondInteract;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer.GetComponent<PlayerMovement>().enabled = false;
        customerCounter.GetComponent<InteractEvent>().enabled = false;
        secondInteract.enabled = false;
        playerUI.SetActive(false);
        //mainCamera.SetActive(false);
        //tutorialCamera.SetActive(true);
        notesUI.SetActive(false);
        blackScreen.SetActive(true);
        leaveTutorial.SetActive(false);
        startBasics.SetActive(false);
        kitchenCamera.SetActive(false);
        startCooking.SetActive(false);
    }

    public void StartMoving()
    {
        thePlayer.GetComponent<PlayerMovement>().enabled = true;
        mainCamera.SetActive(true);
        tutorialCamera.SetActive(false);
        playerUI.SetActive(true);
        leaveTutorial.SetActive(false);
        startBasics.SetActive(false);
    }

    public void StartBasic()
    {
        thePlayer.GetComponent<PlayerMovement>().enabled = false;
        playerUI.SetActive(false);
        startBasics.SetActive(true);

    }

    public void StartCookingBasic()
    {
        thePlayer.GetComponent<PlayerMovement>().enabled = true;
        Destroy(firstInteract);
        secondInteract.enabled = true;
        grandfather.GetComponent<GrandfatherOrder>().PlaceOrder();
        kitchenCamera.SetActive(false);
        mainCamera.SetActive(false);
        tutorialCamera.SetActive(false);
        playerUI.SetActive(true);
        teleporterEnd.SetActive(false);
    }

    public void BasicTraining()
    {
        stage2.Play();
        startBasics.SetActive(false);
    }

    public void OpenNote()
    {
        notesUI.SetActive(true);
    }
    public void CloseNote()
    {
        StartMoving();
        notesUI.SetActive(false);
    }

    public void LeaveTutorial()
    {
        leaveTutorial.SetActive(true);
        thePlayer.GetComponent<PlayerMovement>().enabled = false;
        playerUI.SetActive(false);
    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("Main Game");
    }

    void Update()
    {
        if (mainCamera.activeSelf && numberTimes == 0)
        {
            OpenNote();
            numberTimes = 1;
        }
        if (Input.anyKey && notesUI.activeSelf)
        {
            CloseNote();
        }
       if(startCooking.activeSelf && numberTimes2 == 0)
        {
            StartCookingBasic();
            numberTimes2 = 1;
        }
    }
}
