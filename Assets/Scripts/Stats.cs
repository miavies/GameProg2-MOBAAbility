using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Health
    public float maxHealth;
    public float health;

    //Attack
    public float attackDamage;
    public float attackSpeed;
    public float attackTime;

    //MagicAttack
    public float magicDamage;
    //public float attackSpeed;
    //public float attackTime;

    //    //Heal
    //    public float healthUP;
    //    public float healthSpeed;
    //    public float healthTime;

    //    //Speed
    public float speed;
    public float speedDown;
    //    public float speedSpeed;

    //Status Effect
    private bool effectActive = false;

    HeroCombat heroCombatScript;

    // Start is called before the first frame update
    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();

    }

    // Update is called once per frame
    void Update()
    {
        //Attacking
        if (health <= 0)
        {
            Destroy(gameObject);
            heroCombatScript.targeted = null;
            heroCombatScript.performAttack = false;
        }
    }

    public bool IsEffectActive()
    {
        return effectActive;
    }

    public void SetEffectActive(bool isActive)
    {
        effectActive = isActive;
    }
}
