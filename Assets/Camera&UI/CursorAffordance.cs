﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {


    [SerializeField]
    Texture2D walkCursor = null;

    [SerializeField]
    Texture2D attackCursor = null;

    [SerializeField]
    Texture2D unknownCursor = null;

    [SerializeField]
    Vector2 cursorHotspot = new Vector2(96, 96);

    CameraRaycaster cameraRayCaster;



    // Use this for initialization
    void Start () {
        cameraRayCaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {

       
        

        switch (cameraRayCaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;

            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;

            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                break;

            default:
                Debug.LogError("Dont know what cursor to show");
                return;

        }
        
        Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
	}
}
