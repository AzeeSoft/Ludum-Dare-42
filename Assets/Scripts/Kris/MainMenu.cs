using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject main, htp, cred;

    public enum State
    {
        Main,
        HTP,
        Credits
    }

    public State currentState = State.Main;


    //tomas change
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        switch (currentState)
        {
            case State.Main:
                main.gameObject.SetActive(true);
                htp.gameObject.SetActive(false);
                cred.gameObject.SetActive(false);
                break;
            case State.HTP:
                main.gameObject.SetActive(false);
                htp.gameObject.SetActive(true);
                cred.gameObject.SetActive(false);
                break;
            case State.Credits:
                main.gameObject.SetActive(false);
                htp.gameObject.SetActive(false);
                cred.gameObject.SetActive(true);
                break;
        }

    }

    public void Back()
    {
        currentState = State.Main;
    }

    public void Credits()
    {
        currentState = State.Credits;
    }

    public void HTP()
    {
        currentState = State.HTP;
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void QuitTheGame()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
