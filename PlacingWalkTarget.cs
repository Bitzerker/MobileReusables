using UnityEngine;
using System.Collections;

// This script will place a new GameObject that our player character will follow. 
// Player character should have a TouchController script attached to them.

public class PlacingWalkTarget : MonoBehaviour {

    public bool Allowed = true;
    TouchController controller;

	// Use this for initialization
	void Awake () 
    {
        controller = GetComponent<TouchController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Allowed && Input.GetMouseButtonDown(0) 
            && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() 
            && !ClickableObject.HitClickableThisFrame)
        {
            // User clicked on the screen
            Vector3 mousePos = Input.mousePosition;
            GameObject WalkTarget = new GameObject();
            WalkTarget.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 1f));
            WalkTarget.tag = "WalkTarget";
            WalkTarget.name = "WalkTarget";
            if (controller.WalkTarget != null)
            {
                Destroy(controller.WalkTarget);
            }

            controller.WalkTarget = WalkTarget;
        }

	}
}
