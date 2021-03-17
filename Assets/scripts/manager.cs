using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public GameObject pausa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            encender();
        }
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

}
