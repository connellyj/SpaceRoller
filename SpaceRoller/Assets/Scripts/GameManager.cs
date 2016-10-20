/*
 * Controls the game state
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float cameraSpeed;

    static GameManager instance;

    bool paused;
    int currentScene;
    Vector3 spawnLocation;
    GameObject player;
    CameraController cameraController;

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
        paused = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!paused) Pause();
            else UnPause();
        }
    }

    // Called when any scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        InitLevel();
    }

    // Initializes the level
    void InitLevel() {
        player = FindPlayer();
        if(player != null) InitCamera(player, cameraSpeed);
    }

    // Sets up the camera
    void InitCamera(GameObject thePlayer, float speed) {
        cameraController = Camera.main.gameObject.AddComponent<CameraController>();
        cameraController.InitCamera(thePlayer, speed);
    }

    // Finds the player object and sets the spawn location to its position
    GameObject FindPlayer() {
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        if(thePlayer != null) SetSpawnLocation(thePlayer.transform.position);
        return thePlayer;
    }

    // Pauses the game
    public void Pause() {
        paused = true;
        StopPlayerMovement();
        CreatePopUp("pause");
    }

    // Unpauses the game
    public void UnPause() {
        paused = false;
    }

    // Returns whether or not the game is paused
    public static bool IsGamePaused() {
        return instance.paused;
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

    // Continues the game from the current level
    public static void ContinueGame() {
        if(instance.currentScene == 0) ProceedToNextLevel();
        else SceneManager.LoadScene(instance.currentScene);
    }

    // Restarts the current level
    public static void RetryLevel() {
        instance.StopPlayerMovement();
        instance.player.transform.position = instance.spawnLocation;
        instance.cameraController.ResetCamera();
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
        if(instance.cameraController != null) instance.cameraController.SetInitialPosition(newLocation);
    }

    // Stops the players movement
    void StopPlayerMovement() {
        Rigidbody rb = instance.player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
