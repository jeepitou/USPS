using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResourceUpdater : MonoBehaviour
{
    public Resource resource;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ResourceBank.GetResourceValue(resource).ToString();
    }
}
