using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsClicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickNewGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void clickInstructions() {
        SceneManager.LoadScene("Instructions");
    }

    public void clickQuit() {
        Application.Quit();
    }

}
