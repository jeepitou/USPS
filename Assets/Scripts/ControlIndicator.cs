using TMPro;
using UnityEngine;

public class ControlIndicator : MonoBehaviour
{
    public GameObject CommandText;
    public static string currentCommand = "";
    public static bool isActive = false;

    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = CommandText.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void ShowCommand(string command)
    {
        textMesh.text = command;
        gameObject.SetActive(true);
        isActive = true;
        currentCommand = command;
    }

    public void HideCommand()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
}
