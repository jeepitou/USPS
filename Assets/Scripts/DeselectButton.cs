using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectButton : MonoBehaviour
{
    public void DeselectClickedButton()
    {

        EventSystem.current.SetSelectedGameObject(null);
        
    }
}
