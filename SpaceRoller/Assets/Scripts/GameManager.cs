/* Controls the game state and handles UI events
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float cameraSpeed;
    public GameObject deathPopUp;
    public GameObject pausePopUp;
    public GameObject winPopUp;

    static GameManager instance;

    bool useSpawnLocation;
    bool paused;
    bool acceptPlayerInput;
    int currentScene;
    Vector3 spawnLocation;
    GameObject player;
    GameObject currentPopUp;

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
        /*ONLY NEEDED FOR TESTING PURPOSES ==>>*/ InitLevel();
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        paused = false;
        useSpawnLocation = false;
        acceptPlayerInput = true;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) /*SHOULD BE ADDED IN also last scene && currentScene != 0*/) {
            if(!paused && currentPopUp == null) Pause();
            else if(paused) UnPause();
        }
    }

    // Called when any scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        InitLevel();
    }

    // Initializes the level
    void InitLevel() {
        if(paused) UnPause();
        FindPlayer();
        InitCamera(player, cameraSpeed);
        acceptPlayerInput = true;
    }

    // Sets up the camera
    void InitCamera(GameObject thePlayer, float speed) {
        if(player != null) {
            CameraController cameraController = Camera.main.gameObject.AddComponent<CameraController>();
            cameraController.InitCamera(thePlayer, speed);
        }
    }

    // Finds the player object and moves it to the correct position
    void FindPlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) {
            if(useSpawnLocation) {
                player.transform.position = spawnLocation;
                useSpawnLocation = false;
            }else {
                SetSpawnLocation(player.transform.position);
            }
        }
    }

    // Pauses the game
    public void Pause() {
        paused = true;
        StopAllMovement();
        CreatePopUp("pause");
    }

    // Unpauses the game
    public void UnPause() {
        paused = false;
        RestartAllMovement();
        Destroy(currentPopUp);
    }

    // Stops the movement of everything in the scene
    void StopAllMovement() {
        Time.timeScale = 0;
    }

    // Starts the movement of everything in the scene
    void RestartAllMovement() {
        Time.timeScale = 1;
    }

    // Ends the game when the player dies
    public static void OnPlayerDeath(bool stopMovement) {
        if(stopMovement) instance.StopAllMovement();
        instance.acceptPlayerInput = false;
        instance.CreatePopUp("death");
    }

    // Shows the win pop up
    public static void OnPlayerVictory() {
        instance.StopPlayerMovement();
        instance.acceptPlayerInput = false;
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
        instance.useSpawnLocation = true;
        SceneManager.LoadScene(instance.currentScene);
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

    // Returns whether or not player input should be accepted
    public static bool isAcceptingPlayerInput() {
        return instance.acceptPlayerInput;
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
                currentPopUp = Instantiate(deathPopUp);
                break;
            case "pause":
                currentPopUp = Instantiate(pausePopUp);
                break;
            case "win":
                currentPopUp = Instantiate(winPopUp);
                break;
            default:
                Debug.Log("That type of PopUp does not exist");
                break;
        }
    }
}
