using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    // Connects to the Click_Movement script so the movement functions can be called form this script.
    private Player_Click playerClick;
    // Layer mask set to only count the player's units that are assigned to layer 9.
    public int leftLayerMask = 1 << 9;
    // Layer mask set to only count the locations units can move to that are assigned to layer 10.
    public int RightLayerMask = 1 << 10;
    // 
    public int EnemyLayerMask = 1 << 11;
    // The main camera that acst as the player's eyes.
    public Camera mainCamera;
    // Ray that fires from the camera to the map when they click.
    public Ray mouseRay;
    // Holds were the player has clicked on the map.
    public RaycastHit objectHit;
    // Holds the value of the zoom.
    public float zoomValue = 80f;
    //
    public float zoomChange = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns the connection variable to the specific script on the game object, so that this script does not lose the conenction.
        playerClick = GameObject.Find("GameManager").GetComponent<Player_Click>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the left mouse button has been clicked and calls function if it has.
        if (Input.GetMouseButtonDown(0))
        {
            // Calls left click function that deals with unit slection.
            LeftMouseClick();
        }

        // Checks to see if the right mouse button has been clicked and calls function if it has.
        if (Input.GetMouseButtonDown(1))
        {
            // Calls right click function that deals with unit movement.
            RightMouseClick();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomIn();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOut();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            UpArrowClick();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            DownArrowClick();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftArrowClick();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RightArrowClick();
        }

    }

    // Checks to see if any player unit has been hit by the player left click.
    public void LeftMouseClick()
    {
        // Ray assigned to where the on the screen the mouse is clicked.
        mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Checks to see if ray intersected with any objects on the player layer.
        if (Physics.Raycast(mouseRay, out objectHit, Mathf.Infinity, leftLayerMask))
        {
            // Draws a yellow line from the camera to the clicked location when a player obejct is hit.
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * objectHit.distance, Color.yellow);
            Debug.Log("Left Did Hit");
            // Assigns the clicked player object so the Click_Movement script can access the object.
            playerClick.clickedObject = objectHit.collider.gameObject;
            // Calls the unit selection function.
            playerClick.UnitSelect();
        }
        else
        {
            // Draws a white line from the camera to the clicked location when a player obejct is not hit.
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 1000, Color.white);
            Debug.Log("Left Did not Hit");
        }
    }

    // Checks to see if any movable location has been hit by the player right click.
    public void RightMouseClick()
    {
        // Ray assigned to where the on the screen the mouse is clicked.
        mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Additonal if statment needed here to check is the object hit was enemy. 
        if (Physics.Raycast(mouseRay, out objectHit, Mathf.Infinity, EnemyLayerMask))
        {
            // Draws a red line from the camera to the clicked enemy when an enemy is hit.
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * objectHit.distance, Color.red);
            Debug.Log("Right Did Hit an Enemy");
            // Assigns the clicked location so the Click_Movement script can access the location.
            playerClick.chosenEnemy = objectHit.collider.gameObject; 
            // Calls the unit movement to enemy function.
            playerClick.MoveToEnemy();
        }
        // Checks to see if ray intersected with any objects on the movable location layer.
        else if (Physics.Raycast(mouseRay, out objectHit, Mathf.Infinity, RightLayerMask))
        {
            // Draws a blue line from the camera to the clicked location when a movable location is hit.
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * objectHit.distance, Color.blue);
            Debug.Log("Right Did Hit an Location");
            // Assigns the clicked location so the Click_Movement script can access the location.
            playerClick.moveLocation = mouseRay.GetPoint(objectHit.distance);
            // Calls the unit movement function.
            playerClick.MoveUnits();
        }
        else
        {
            // Draws a white line from the camera to the clicked location when a movable location is not hit.
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 1000, Color.white);
            Debug.Log("Right Did not Hit");
        }

    }

    public void ZoomIn()
    {
        mainCamera.GetComponent<Camera>().fieldOfView--;
    }

    public void ZoomOut()
    {
        mainCamera.GetComponent<Camera>().fieldOfView++;
    }

    public void UpArrowClick()
    {
        Debug.Log("Up Click");
        //mainCamera.transform.position.x = 2f;
        mainCamera.transform.Translate(Vector3.up * 10 * Time.deltaTime);
    }

    public void DownArrowClick()
    {
        Debug.Log("Down Click");
        mainCamera.transform.Translate(Vector3.down * 10 * Time.deltaTime);
    }

    public void LeftArrowClick()
    {
        Debug.Log("Left Click");
        mainCamera.transform.Translate(Vector3.left * 10 * Time.deltaTime);
    }

    public void RightArrowClick()
    {
        Debug.Log("Right Click");
        mainCamera.transform.Translate(Vector3.right * 10 * Time.deltaTime);
    }
}
