using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
   
    // Update is called once per frame
    void Update()
    {
        speed = gameObject.GetComponent<Stats>().speed;
        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);

        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;

    }
}
