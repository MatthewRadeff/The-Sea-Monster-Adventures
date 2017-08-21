using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    // selects the scene to go to when the button is clicked
    public void ChangeScene(string m_sceneName)
    {

        SceneManager.LoadScene(m_sceneName);
    }


    // quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

}
