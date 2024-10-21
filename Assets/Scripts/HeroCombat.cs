using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public enum HeroActionType { Attack, Support, Debilitate, Ultimate };
    public HeroActionType heroActionType;

    public GameObject targeted;
    public float attackRange;
    public float rotateSpeedforAttack;

    private Movement moveScript;
    private Stats statsScript;
    private Animator anim;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performAttack = true;

    void Start()
    {
        moveScript = GetComponent<Movement>();
        statsScript = GetComponent<Stats>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (targeted != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targeted.transform.position) > attackRange)
            {
                moveScript.agent.SetDestination(targeted.transform.position);
                moveScript.agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targeted.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedforAttack * Time.deltaTime * 5);

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if (heroActionType == HeroActionType.Attack)
                {
                    if (performAttack)
                    {
                        StartCoroutine(AttackInterval());
                    }
                }
            }
        }
    }

    //Basic Attack
    IEnumerator AttackInterval()
    {
        performAttack = false;
        anim.SetBool("Attack", true);

        yield return new WaitForSeconds(statsScript.attackTime);


        if (targeted == null)
        {
            anim.SetBool("Attack", false);
            performAttack = true;
        }
    }

    public void Attack()
    {
        if (targeted != null)
        {
            if (targeted.GetComponent<Targetable>().TargetType == Targetable.TargetableType.Enemy)
            {
                targeted.GetComponent<Stats>().health -= statsScript.attackDamage;
            }
        }

        performAttack = true;
    }
}
