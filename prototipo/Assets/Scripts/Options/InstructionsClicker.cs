using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsClicker : MonoBehaviour
{
    public Canvas startingCanvas;
    public Canvas secondCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickNext() {
        startingCanvas.gameObject.SetActive(false);
        secondCanvas.gameObject.SetActive(true);
    }

    public void clickClose() {
        SceneManager.LoadScene("MainMenu");
    }
}
