using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour {

    public int offsetX = 2;
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool reverseScale = false;

    private float scaledSpriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    private void Awake () {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start () {
        scaledSpriteWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x * Mathf.Abs(myTransform.localScale.x);
	}
	
	void Update () {
        if (!hasARightBuddy || !hasALeftBuddy)
        {
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
            if (!hasARightBuddy){
                float edgeVisiblePositionRight = (myTransform.position.x + scaledSpriteWidth / 2) - camHorizontalExtent;
                if (!hasARightBuddy && cam.transform.position.x >= edgeVisiblePositionRight - offsetX) {
                    MakeNewBuddy(1);
                }
            }
            if (!hasALeftBuddy) {
                float edgeVisiblePositionLeft = (myTransform.position.x - scaledSpriteWidth / 2) + camHorizontalExtent;
                if (!hasALeftBuddy && cam.transform.position.x <= edgeVisiblePositionLeft + offsetX) {
                    MakeNewBuddy(-1);
                }
            }
        }
    }

    void MakeNewBuddy (int rightOrLeft) {
        Vector3 newPosition = new Vector3(myTransform.position.x + scaledSpriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = (Transform) Instantiate(myTransform, newPosition, myTransform.rotation);
        if (reverseScale) {
            newBuddy.localScale = new Vector3(-newBuddy.localScale.x, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0) {
            hasARightBuddy = true;
            newBuddy.GetComponent<Tiling>().SetLeftBuddy();
        } else {
            hasALeftBuddy = true;
            newBuddy.GetComponent<Tiling>().SetRightBuddy();
        }
    }

    void SetRightBuddy() {
        hasARightBuddy = true;
    }

    void SetLeftBuddy()
    {
        hasALeftBuddy = true;
    }
}
