using System.Collections;
using UnityEngine;

public class SlowAndDamageArea : MonoBehaviour
{
    public float slowPercentage;
    public float physicalAttack;
    public float magicalAttack;
    public float slowDuration;
    public float coolDownDuration;

    private Collider areaCollider;
    private Stats playerStats;
    private Abilities playerAbilities;

    private void Start()
    {
        areaCollider = GetComponent<Collider>();
        if (areaCollider == null)
        {
            Debug.LogError("No collider attached to this object!");
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<Stats>();
            playerAbilities = player.GetComponent<Abilities>();

            if (playerStats == null || playerAbilities == null)
            {
                Debug.LogError("Player Stats or Abilities component missing!");
            }
        }
        else
        {
            Debug.LogError("Player object with 'Player' tag not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Stats enemy = other.GetComponent<Stats>();
            if (enemy != null && !enemy.IsEffectActive())
            {
                // Get slow and damage values from the player's stats and abilities
                slowPercentage = playerStats.speedDown;
                slowDuration = playerAbilities.skill2Timer;
                coolDownDuration = playerAbilities.cooldown2;
                physicalAttack = playerStats.attackDamage;
                magicalAttack = playerStats.magicDamage;

                // Apply the effect to the enemy
                StartCoroutine(ApplySlowAndDamage(enemy, other.GetComponent<Animator>()));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Stats enemy = other.GetComponent<Stats>();
            if (enemy != null && !enemy.IsEffectActive())
            {
                // Get slow and damage values from the player's stats and abilities
                slowPercentage = playerStats.speedDown;
                slowDuration = playerAbilities.skill2Timer;
                coolDownDuration = playerAbilities.cooldown2;
                physicalAttack = playerStats.attackDamage;
                magicalAttack = playerStats.magicDamage;

                // Apply the effect to the enemy
                StartCoroutine(ApplySlowAndDamage(enemy, other.GetComponent<Animator>()));
            }
        }
    }

    private IEnumerator ApplySlowAndDamage(Stats enemy, Animator enemyAnimator)
    {
        float originalSpeed = enemy.speed;
        float reducedSpeed = originalSpeed * (100 - slowPercentage) / 100;

        enemy.speed = reducedSpeed;

        float originalAnimatorSpeed = enemyAnimator.speed;
        enemyAnimator.speed = originalAnimatorSpeed * (100 - slowPercentage) / 100;

        enemy.health -= (physicalAttack + (0.7f * playerStats.magicDamage));
        enemy.SetEffectActive(true);

        yield return new WaitForSeconds(slowDuration - 1f);

        // Restore the enemy's speed
        enemy.speed = originalSpeed;
        enemyAnimator.speed = originalAnimatorSpeed;

        yield return new WaitForSeconds(coolDownDuration);
        enemy.SetEffectActive(false);
    }
}
