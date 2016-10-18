using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public string[] path;
    public bool reverse;
    public float speed;
    
    string[,] movementMap;

    void Start() {
        movementMap = ParseMovementMap(path);
        StartCoroutine(AnimateObject());
    }

    // Parses the inputted movement
    string[,] ParseMovementMap(string[] pathToParse) {
        string [,] map = new string[2, pathToParse.Length];
        for(int i = 0; i < pathToParse.Length; i++) {
            string[] parsedString = pathToParse[i].Split(' ');
            map[0, i] = parsedString[0];
            map[1, i] = parsedString[1];
        }
        return map;
    }

    // Gets the number of grids the object should move for the given part of the path
    float GetGridCount(int pathIndex) {
        float count;
        float.TryParse(movementMap[1, pathIndex], out count);
        return count;
    }

    // Gets the direction the object should move for the given part of the path
    Vector3 GetObjectDirection(int pathIndex, bool inReverse) {
        switch(movementMap[0, pathIndex]) {
            case "up":
                if(inReverse) return Vector3.down;
                return Vector3.up;
            case "down":
                if(inReverse) return Vector3.up;
                return Vector3.down;
            case "right":
                if(inReverse) return Vector3.left;
                return Vector3.right;
            case "left":
                if(inReverse) return Vector3.right;
                return Vector3.left;
            case "forward":
                if(inReverse) return Vector3.back;
                return Vector3.forward;
            case "back":
                if(inReverse) return Vector3.forward;
                return Vector3.back;
            default:
                Debug.Log("Incorrectly inputted person movement");
                return Vector3.zero;
        }
    }
    
    IEnumerator AnimateObject() {
        int pathIndex = 0;
        float gridCount = 0;
        bool inReverse = false;
        Vector3 direction = Vector3.zero;
        while(true) {
            if(gridCount == 0 && path.Length != 0) {
                gridCount = GetGridCount(pathIndex);
                direction = GetObjectDirection(pathIndex, inReverse);

                // Updates the index
                if(!inReverse) {
                    pathIndex++;
                } else {
                    if(pathIndex == 0) {
                        inReverse = false;
                    } else {
                        pathIndex--;
                    }
                }
                if(pathIndex == path.Length) {
                    if(reverse) {
                        inReverse = true;
                        pathIndex--;
                    } else {
                        pathIndex = 0;
                    }
                }
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