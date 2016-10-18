/*
 * Adds functionality to the buttons on the start screen
 */ 

using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {

    void Start() {
        // Start button functionality
        Button startButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(() => GameManager.ProceedToNextLevel());
    }
}
