using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject ship;
    public GameObject gameoverMessage;
    public GameObject playerCharacter;
    public GameObject playerCursor;
    public GameObject scoreMarker;
    public int score = 0;

    private TextMeshProUGUI _score;
    private bool _gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ship.GetComponent<FuelController>().TankIsEmpty += TankIsEmpty;
        _score = scoreMarker.GetComponent<TextMeshProUGUI>();
    }

    void TankIsEmpty()
    {
        StartCoroutine(VerifyIfLandedOnFuelPlanet());
    }

    public void UpdateScore()
    {
        _score.text = "00000000".Substring(score.ToString().Length) + score.ToString();
        
    }

    IEnumerator VerifyIfLandedOnFuelPlanet()
    {
        yield return new WaitForSeconds(2);
        if (ship.GetComponent<FuelController>().IsEmpty())
        {
            GameOver("");
        }
    }

    // Update is called once per frame
    public void GameOver(string mes)
    {
        if (mes != "")
        {
            gameoverMessage.GetComponentInChildren<TextMeshProUGUI>().text = mes;
        }

        gameoverMessage.SetActive(true);
        playerCursor.SetActive(false);
        _gameOver = true;
    }

    private void Update()
    {
        if (_gameOver)
        {
            gameoverMessage.SetActive(true);
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void Crash()
    {
        GameOver("");
    }

}
