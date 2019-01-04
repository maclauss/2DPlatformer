using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

    private GameObject[] chests;

    void Awake() {
        chests = GameObject.FindGameObjectsWithTag("Chest");
    }

    void Update() {
        if (Input.GetKeyDown("e")) {
            GameObject closest = findClosestObject(chests);
            if (Mathf.Abs(transform.position.x - closest.transform.position.x) <= 1) {
                string item = ((Chest)closest.GetComponent(typeof(Chest))).Open();
            }
        }
    }

    GameObject findClosestObject(GameObject[] objects){
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject obj in objects) {
            float distance = Vector3.Distance(obj.transform.position, currentPos);
            if (distance < minDist) {
                closest = obj;
                minDist = distance;
            }
        }
        return closest;
    }
}
