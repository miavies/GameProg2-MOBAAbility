using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    Stats statsScript;

    // Start is called before the first frame update
    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = statsScript.maxHealth;
        playerSlider3D.maxValue = statsScript.maxHealth;
        statsScript.health = statsScript.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = statsScript.health;
        playerSlider3D.value = playerSlider2D.value;
    }
}
