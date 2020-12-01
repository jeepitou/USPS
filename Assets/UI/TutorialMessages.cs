using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialMessages : MonoBehaviour
{
    public Resource tutorial;
    string[] tutorialMessages = new string[] {
        "DON'T  LET  ANY  PLANET  RUN  OUT  OF  RESSOURCE",
        "USE  WASD  TO  CONTROL  THE  SHIP",
        "USE  M  TO  VIEW  THE  MAP",
        "MAP  SHOWS  YOU  WHAT  TYPE  OF RESSOURCE  EACH  PLANET NEED  AND  PRODUCE",
        "IT  ALSO  SHOWS  YOU  THE  RESSOURCE  METER.  YOU  WILL  LOSE  IF  ANY  PLANET  HAS  NO  MORE  RESSOURCE",
        "LAND  ON  A  PLANET  TO  START  GATHERING  RESOURCE",
        "KEEP  AN  EYE  ON  YOUR  GAZ  LEVEL"};

    int nextMessage = 0;
    TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorial.resourceName != "")
        {
            gameObject.SetActive(false);
            return;
        }
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowNextMessage()
    {
        
        nextMessage += 1;
        if (tutorialMessages.Length-1 < nextMessage)
        {
            GetComponent<Animator>().SetBool("TutorialDone", true);
            tutorial.resourceName = "done";
            gameObject.SetActive(false);
            return;
        }
        _text.text = tutorialMessages[nextMessage];
    }
}
