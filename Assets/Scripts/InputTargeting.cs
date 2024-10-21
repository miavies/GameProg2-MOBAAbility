using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Targeting Enemy
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //If enemy or ally is selected make it a target
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Targetable>().TargetType == Targetable.TargetableType.Enemy || hit.collider.gameObject.GetComponent<Targetable>().TargetType == Targetable.TargetableType.Ally)
                    {
                        selectedHero.GetComponent<HeroCombat>().targeted = hit.collider.gameObject;
                    }
                }

                else if (hit.collider.gameObject.GetComponent<Targetable>() == null)
                {
                    selectedHero.GetComponent<HeroCombat>().targeted = null;
                }
            }
        }
    }
}
