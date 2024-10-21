using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealth : MonoBehaviour
{
    public Slider targetSlider3D;
    Stats statsScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject foundObject = GameObject.FindGameObjectWithTag("Enemy");

        if (foundObject == null)
        {
            foundObject = GameObject.FindGameObjectWithTag("Ally");
        }

        if (foundObject != null)
        {
            statsScript = foundObject.GetComponent<Stats>();
            targetSlider3D.maxValue = statsScript.maxHealth;
            statsScript.health = statsScript.maxHealth;
        }
        else
        {
            Debug.LogError("No object with 'Enemy' or 'Ally' tag found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetSlider3D.value = statsScript.health;
    }
}
