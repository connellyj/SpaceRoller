using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public string[] path;
    public bool reverse;
    public float speed;
    public Vector3 direction;
    bool shouldMove;
    string[,] movementMap;

    void Start() {
        shouldMove = true;
        movementMap = new string[2, path.Length];
        for(int i = 0; i < path.Length; i++) {
            string[] parsedString = path[i].Split(' ');
            movementMap[0, i] = parsedString[0];
            movementMap[1, i] = parsedString[1];
        }
        StartCoroutine(MovePerson());
    }


    // Moves the person
    IEnumerator MovePerson() {
        // indexes the path
        int index = 0;
        // checks when the person has moved the alloted distance
        float gridCount = 0;
        // tracks whether or not currently reversing the path
        bool inReverse = false;

        // "animation" while loop
        while(true) {
            if(gridCount == 0 && path.Length != 0) {
                // figrues out how many grid squares to traverse
                float.TryParse(movementMap[1, index], out gridCount);
                // figures out the direction to move in
                switch(movementMap[0, index]) {
                    case "up":
                        if(inReverse) SetDirection(Vector3.down);
                        else SetDirection(Vector3.up);
                        break;
                    case "down":
                        if(inReverse) SetDirection(Vector3.up);
                        else SetDirection(Vector3.down);
                        break;
                    case "right":
                        if(inReverse) SetDirection(Vector3.left);
                        else SetDirection(Vector3.right);
                        break;
                    case "left":
                        if(inReverse) SetDirection(Vector3.right);
                        else SetDirection(Vector3.left);
                        break;
                    case "forward":
                        if(inReverse) SetDirection(Vector3.back);
                        else SetDirection(Vector3.forward);
                        break;
                    case "back":
                        if(inReverse) SetDirection(Vector3.forward);
                        else SetDirection(Vector3.back);
                        break;
                    default:
                        Debug.Log("Incorrectly inputted person movement");
                        break;
                }

                // adjusts the index based
                if(!inReverse) {
                    index++;
                } else {
                    if(index == 0) {
                        inReverse = false;
                    } else {
                        index--;
                    }
                }
                if(index == path.Length) {
                    if(reverse) {
                        inReverse = true;
                        index--;
                    } else {
                        index = 0;
                    }
                }
            }

            // moves the person
            if(shouldMove) {
                if(gridCount - speed < 0) {
                    transform.position += direction * gridCount;
                    gridCount = 0;
                } else {
                    transform.position += direction * speed;
                    gridCount -= speed;
                }
                yield return new WaitForSeconds(0.05f);
            } else {
                yield return null;
            }

        }
    }

    void SetDirection(Vector3 newDirection) {
        direction = newDirection;
    }
}