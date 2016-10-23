/* Adds functionality to the buttons on the start screen
 * Meant to be attached to the parent of the buttons
 */ 

using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {

    void Start() {
        foreach(Transform child in transform) {
            switch(child.tag) {
                case "StartButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(() => GameManager.ContinueGame());
                    break;
                case "RestartButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(() => GameManager.RestartGame());
                    break;
                case "QuitButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(() => GameManager.QuitGame());
                    break;
                default:
                    break;
            }
        }
    }
}
