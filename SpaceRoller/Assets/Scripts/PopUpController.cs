/*
 * Adds functionality to buttons on pop up menus
 */ 

using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {

    void Awake() {
        foreach(Transform child in transform) {
            switch(child.tag) {
                case "RetryButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(() => {
                        GameManager.RetryLevel();
                        Destroy(transform.parent.gameObject);
                    });
                    break;
                case "MainMenuButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(() => {
                        GameManager.GoToMainMenu();
                        Destroy(transform.parent.gameObject);
                    });
                    break;
                case "NextLevelButton":
                    child.gameObject.GetComponent<Button>().onClick.AddListener(() => {
                        GameManager.ProceedToNextLevel();
                        Destroy(transform.parent.gameObject);
                    });
                    break;
                default:
                    break;
            }
        }
    }
}
