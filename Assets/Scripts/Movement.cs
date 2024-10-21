using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float rotateSpeedMovement = 1f;
    public float rotateVelocity;

    public HeroCombat heroCombatScript;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    //    // Update is called once per frame
    void Update()
    {
        if (heroCombatScript.targeted != null)
        {
            if (heroCombatScript.targeted.GetComponent<HeroCombat>() != null)
            {
                if (!heroCombatScript.targeted.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombatScript.targeted = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Floor")
                {
                    //Movement
                    agent.SetDestination(hit.point);
                    heroCombatScript.targeted = null;
                    agent.stoppingDistance = 0;

                    //Rotation
                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * Time.deltaTime * 5);

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }


            }
        }
    }
}
