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
    public bool unitMoving;
    public float unitRange;
    public int unitSpeed;
    public int unitHealth;
    public int unitDamage;
    public GameObject unitTarget;
    public Vector3 targetLocation;
    public GameObject unitEnemy;
    public float enemyDistance;
    // Layer mask set to only count the enemy objects that are assigned to layer 11.
    public int enemyLayerMask = 11;


    // Start is called before the first frame update
    void Start()
    {
        playerClick = GameObject.Find("GameManager").GetComponent<Player_Click>();
        objectAgent = GetComponent<NavMeshAgent>();

        if (gameObject.transform.position == targetLocation)
        {
            targetLocation = gameObject.transform.position;
            unitMoving = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (unitTarget != null)
        {
            EnemySightCheck();
        }

        if (unitEnemy != null)
        {
            enemyDistance = Vector3.Distance(unitEnemy.transform.position, transform.position);

            if (enemyDistance > unitRange)
            {
                Debug.Log("Target Lost " + unitEnemy.gameObject);
                unitEnemy = null;
            }
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
        unitMoving = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");

        if (other.gameObject.layer == enemyLayerMask)
        {
            Debug.Log("Enemy Seen " + other.gameObject);

            if (unitEnemy == null)
            {
                Debug.Log("Target Locked " + other.gameObject);
                unitEnemy = other.gameObject;
            }

            if (other.gameObject == unitTarget)
            {
                targetLocation = gameObject.transform.position;
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
        else
        {
            targetLocation = gameObject.transform.position;
        }
    }

}
