/* A script used to create arbitrary paths easily for game objects
 */

using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public string[] path;
    public bool reverse;
    public float speed;

    void Start() {
        string[,] movementMap = ParseMovementMap(path);
        StartCoroutine(AnimateObject(movementMap));
    }

    // Parses the inputted movement
    string[,] ParseMovementMap(string[] pathToParse) {
        string[,] map = new string[2, pathToParse.Length];
        for(int i = 0; i < pathToParse.Length; i++) {
            string[] parsedString = pathToParse[i].Split(' ');
            map[0, i] = parsedString[0];
            map[1, i] = parsedString[1];
        }
        return map;
    }

    // Gets the direction the object should move for the given part of the path
    Vector3 GetObjectDirection(int pathIndex, bool inReverse, string[,] movementMap) {
        int reversed;
        if(inReverse) reversed = -1;
        else reversed = 1;
        switch(movementMap[0, pathIndex]) {
            case "up":
                return Vector3.up * reversed;
            case "down":
                return Vector3.down * reversed;
            case "right":
                return Vector3.right * reversed;
            case "left":
                return Vector3.left * reversed;
            case "forward":
                return Vector3.forward * reversed;
            case "back":
                return Vector3.back * reversed;
            default:
                Debug.Log("Incorrectly inputted person movement");
                return Vector3.zero;
        }
    }
    
    // Moves the object based on the given map
    IEnumerator AnimateObject(string[,] movementMap) {
        int pathIndex = 0;
        float gridCount = 0;
        bool inReverse = false;
        Vector3 direction = Vector3.zero;
        while(true) {
            if(gridCount == 0 && path.Length != 0) {
                float.TryParse(movementMap[1, pathIndex], out gridCount);
                direction = GetObjectDirection(pathIndex, inReverse, movementMap);

                // Updates the index
                if(reverse) {
                    if(pathIndex == path.Length - 1 && !inReverse) {
                        inReverse = true;
                    } else if(pathIndex == 0 && inReverse) {
                        inReverse = false;
                    } else if(inReverse) {
                        pathIndex = (path.Length + pathIndex - 1) % path.Length;
                    } else {
                        pathIndex = (pathIndex + 1) % path.Length;
                    }
                }
                else pathIndex = (pathIndex + 1) % path.Length;
            }

            // Moves the object
            if(gridCount - speed < 0) {
                transform.position += direction * gridCount;
                gridCount = 0;
            } else {
                transform.position += direction * speed;
                gridCount -= speed;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}