using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Info : MonoBehaviour
{
    private Player_Click playerClick;
    public LayerMask playerUnit;
    public bool objectSelected;
    public Vector3 moveLocation;
    public NavMeshAgent objectAgent;
    public int unitSpeed;
    public int unitHealth;
    public int unitDamage;
    public GameObject unitTarget;
    public Vector3 targetLocation;
    // Layer mask set to only count the enemy objects that are assigned to layer 11.
    public int EnemyLayerMask = 11;


    // Start is called before the first frame update
    void Start()
    {
        playerClick = GameObject.Find("GameManager").GetComponent<Player_Click>();
        objectAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unitTarget != null)
        {
            EnemySightCheck();
        }
    }

    public void UnitMovement()
    {
        if (unitTarget != null)
        {
        targetLocation = unitTarget.transform.position;
        }

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

    public void EnemySightCheck()
    {
        Debug.Log("Checking for Enemy");

        if (unitTarget != null)
        {
            // Check if unit can still see target, if not stop moving.

            playerClick.MoveToEnemy();
        }
    }

}
