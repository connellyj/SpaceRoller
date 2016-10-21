/*
 * Controls the game state
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float cameraSpeed;
    public GameObject deathPopUp;
    public GameObject pausePopUp;
    public GameObject winPopUp;

    static GameManager instance;

    bool isLevelOver;
    bool acceptPlayerInput;
    int currentScene;
    Vector3 spawnLocation;
    Vector3[] oldPlayerMovement;
    GameObject player;
    CameraController cameraController;
    GameObject curPausePopUp;

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
        acceptPlayerInput = true;
        isLevelOver = false;
    }

    void Update() {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(Input.GetKeyDown(KeyCode.Escape) && !isLevelOver && curSceneIndex != 0) {
            if(acceptPlayerInput) Pause();
            else UnPause();
        }
    }

    // Called when any scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        InitLevel();
    }

    // Initializes the level
    void InitLevel() {
        instance.isLevelOver = false;
        acceptPlayerInput = true;
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
        StopAllMovement();
        curPausePopUp = CreatePopUp("pause");
    }

    // Unpauses the game
    public void UnPause() {
        acceptPlayerInput = true;
        RestartPlayerMovement(oldPlayerMovement);
        Time.timeScale = 1;
        Destroy(curPausePopUp);
    }

    void StopAllMovement() {
        acceptPlayerInput = false;
        oldPlayerMovement = StopPlayerMovement();
        Time.timeScale = 0;
    }

    // Returns whether or not the game is paused
    public static bool IsPlayerInputAccepted() {
        return instance.acceptPlayerInput;
    }

    // Ends the game when the player dies
    public static void OnPlayerDeath(bool stopMovement) {
        if(!instance.isLevelOver) {
            if(stopMovement) instance.StopAllMovement();
            instance.isLevelOver = true;
            instance.CreatePopUp("death");
        }
    }

    // Shows the win pop up
    public static void OnPlayerVictory() {
        instance.acceptPlayerInput = false;
        instance.isLevelOver = true;
        instance.StopPlayerMovement();
        instance.CreatePopUp("win");
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
        instance.isLevelOver = false;
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
    Vector3[] StopPlayerMovement() {
        Vector3[] oldSpeed = new Vector3[2];
        Rigidbody rb = instance.player.GetComponent<Rigidbody>();
        oldSpeed[0] = rb.velocity;
        rb.velocity = Vector3.zero;
        oldSpeed[1] = rb.angularVelocity;
        rb.angularVelocity = Vector3.zero;
        return oldSpeed;
    }

    // Gives the player the given velocity
    void RestartPlayerMovement(Vector3[] movement) {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = movement[0];
        rb.angularVelocity = movement[1];
    }

    // Creates the given UI pop-up
    GameObject CreatePopUp(string type) {
        switch(type) {
            case "death":
                return Instantiate(deathPopUp);
            case "pause":
                return Instantiate(pausePopUp);
            case "win":
                return Instantiate(winPopUp);
            default:
                Debug.Log("That type of PopUp does not exist");
                return null;
        }
    }
}
