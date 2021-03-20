using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public GameObject pausa;
    public GameObject winScreen;
    public GameObject loseScreen;
    public Text pickUpCounter;
    public GameObject pickUpContainter;
    public PlayerHealth health;

    public int playerScore = 0;
    int maxScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        foreach (var item in pickUpContainter.transform)
        {
            maxScore += 1;
        }
        UpdatePickUpCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!winScreen.activeSelf && !loseScreen.activeSelf)
            {
                encender();
            }
            else if (winScreen.activeSelf || loseScreen.activeSelf)
            {
                regresarmenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (loseScreen.activeSelf)
            {
                reloadLevel();
            }
            else if (winScreen.activeSelf)
            {
                loadNextLevel();
            }
        }
    }
    private void LateUpdate()
    {
        UpdatePickUpCounter();
        playerStateCheck();
    }

    public void encender()
    {
        if (pausa)
        {
            pausa.SetActive(!pausa.activeSelf);
            Time.timeScale = pausa.activeSelf ? 0.0f : 1.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void reanudar()
    {
        if (pausa)
        {
            pausa.SetActive(!pausa.activeSelf);
            Time.timeScale = pausa.activeSelf ? 0.0f : 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void regresarmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void addToScore(int amount)
    {
        playerScore += amount;
    }

    void playerStateCheck()
    {
        if (playerScore == maxScore)
        {
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (health.currentHealth == 0)
        {
            loseScreen.SetActive(true);
            winScreen.SetActive(false);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdatePickUpCounter()
    {
        pickUpCounter.text = "Pick Ups: " + playerScore + " / " + maxScore;
    }
}
