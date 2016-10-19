using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float cameraSpeed;

    static GameManager instance;

    int currentScene;
    Vector3 spawnLocation;
    GameObject player;

    void Awake() {
        // Makes sure there is only one GameManager in each scene
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    void Start() {
        InitLevel();
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentScene = 0;
    }

    // Called when any scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        InitLevel();
    }

    void InitLevel() {
        player = FindPlayer();
        if(player != null) InitCamera(player, cameraSpeed);
    }

    // Sets up the camera
    void InitCamera(GameObject thePlayer, float speed) {
        CameraController cameraController = Camera.main.gameObject.AddComponent<CameraController>();
        cameraController.InitCamera(thePlayer, speed);
    }

    // Finds the player object and sets the spawn location to its position
    GameObject FindPlayer() {
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        if(thePlayer != null) SetSpawnLocation(thePlayer.transform.position);
        return thePlayer;
    }

    // Ends the game when the player dies
    public static void OnPlayerDeath() {
        instance.CreatePopUp("death");
    }

    // Moves on to the next level
    public static void ProceedToNextLevel() {
        instance.currentScene++;
        SceneManager.LoadScene(instance.currentScene);
    }

    // Restarts the curret level
    public static void RetryLevel() {
        instance.player.transform.position = instance.spawnLocation;
    }

    // Quits the game
    public static void QuitGame() {
        Application.Quit();
    }

    // Restarts the game from the very beginning
    public static void RestartGame() {
        instance.currentScene = 0;
        ProceedToNextLevel();
    }

    // Returns to the start screen
    public static void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    // Sets the location the player should respawn at
    public static void SetSpawnLocation(Vector3 newLocation) {
        instance.spawnLocation = newLocation;
    }

    // Creates the given UI pop-up
    void CreatePopUp(string type) {
        switch(type) {
            case "death":
                break;
            case "pause":
                break;
            case "win":
                break;
            default:
                Debug.Log("That type of PopUp does not exist");
                break;
        }
    }
}
