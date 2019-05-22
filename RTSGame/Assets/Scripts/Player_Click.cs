using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Click : MonoBehaviour
{
    private Unit_Info unitInfo;
    public GameObject[] playersObjects;
    public GameObject[] movingObjects;
    public GameObject clickedObject;
    public bool objectSelected;
    public Transform startLocation;
    public Vector3 moveLocation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnitSelect()
    {
        unitInfo = clickedObject.GetComponent<Unit_Info>();

        objectSelected = false;

        for (int i = 0; i < movingObjects.Length; i++)
        {
            if (movingObjects[i] == clickedObject)
            {
                objectSelected = true;
            }
        }

        if (objectSelected == true)
        {
            for (int i = 0; i < movingObjects.Length; i++)
            {
                if (movingObjects[i] == clickedObject)
                {
                    movingObjects[i] = null;
                    unitInfo.objectSelected = false;
                    Debug.Log("Object Unselected " + clickedObject);
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < movingObjects.Length; i++)
            {
                if (movingObjects[i] == null)
                {
                    movingObjects[i] = clickedObject;
                    unitInfo.objectSelected = true;
                    Debug.Log("Object Selected " + clickedObject);
                    return;
                }
            }
        }
    }

    public void MoveUnits()
    {
        for (int i = 0; i < movingObjects.Length; i++)
        {
            if (movingObjects[i] != null)
            {
                unitInfo = movingObjects[i].GetComponent<Unit_Info>();
                unitInfo.moveLocation = moveLocation;
                unitInfo.UnitMovement();
            }
        }

    }
}
