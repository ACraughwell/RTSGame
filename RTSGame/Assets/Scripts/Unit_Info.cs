using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Info : MonoBehaviour
{
    public LayerMask playerUnit;
    public bool objectSelected;
    public Vector3 moveLocation;
    public UnityEngine.AI.NavMeshAgent objectAgent;
    public int unitSpeed;
    public int unitHealth;
    public int unitDamage;
    public GameObject unitTarget;

    // Layer mask set to only count the enemy objects that are assigned to layer 11.
    private int EnemyLayerMask = 11;


    // Start is called before the first frame update
    void Start()
    {
        objectAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unitTarget != null)
        {
            if (GameObject.Find(unitTarget.name) != null)
            {
                objectAgent.destination = unitTarget.transform.position;
            }
            else
            {
                unitTarget = null;
            }
        }
    }

    public void UnitMovement()
    {
        Debug.Log("Unit " + gameObject + " Moving");
        objectAgent.destination = moveLocation;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");

        if (other.gameObject.layer == EnemyLayerMask)
        {
            Debug.Log("Enemy Seen " + other.gameObject);

            if (unitTarget == null)
            {
                Debug.Log("Target Locked " + other.gameObject);
                unitTarget = other.gameObject;
            }
        }
    }
}
