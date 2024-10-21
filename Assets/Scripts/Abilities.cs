using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Abilities : MonoBehaviour
{
    //Skill 1
    [Header("Skill 1")]
    public Image skillImage1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode skill1;

    Vector3 position;
    public Canvas skill1canvas;
    public Image skillshot;
    public Transform player;

    //Skill 2
    [Header("Skill 2")]
    public Image skillImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public KeyCode skill2;

    public GameObject newtargetCircleArea;
    public GameObject targetCircleArea;
    public Image targetCircle;
    public Image indicatorRangeCirlce;
    public Canvas skill2canvas;
    private Vector3 posUp;
    public float maxSkill2Distance;

    private float disableTargetCircleTimer = 0f;
    public float skill2Timer = 1.5f;

    //Ultimate
    [Header("Ultimate")]
    public Image ultimateImage;
    public float cooldownUlt = 30;
    bool isCooldownUlt = false;
    public KeyCode ultimate;

    
    public HeroCombat heroCombatScript;

    // Start is called before the first frame update
    void Start()
    {
        skillImage1.fillAmount = 0;
        skillImage2.fillAmount = 0;
        ultimateImage.fillAmount = 0;

        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCirlce.GetComponent<Image>().enabled = false;

    }



    //    // Update is called once per frame
    void Update()
    {
        Skill1();
        Skill2();
        Ultimate();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Skill 1
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z); 
        }

        // Skill 2
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z); 
            }
        }

        // Skill 1 Canvas
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        skill1canvas.transform.rotation = Quaternion.Lerp(transRot, skill1canvas.transform.rotation, 0f);

        // Skill 2 Canvas
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxSkill2Distance);

        var newHitPos = transform.position + hitPosDir * distance;
        skill2canvas.transform.position = new Vector3(newHitPos.x, 8f, newHitPos.z);
    }


    void Skill1()
    {
        if (Input.GetKey(skill1) && isCooldown1 == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
            indicatorRangeCirlce.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
        }

        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown1 = true;
            skillImage1.fillAmount = 1;
        }

        if (isCooldown1)
        {
            skillImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            skillshot.GetComponent<Image>().enabled = false;

            if (skillImage1.fillAmount <= 0)
                {
                    skillImage1.fillAmount = 0;
                    isCooldown1 = false;
                }
        }
    }

    void Skill2()
    {
        if (Input.GetKey(skill2) && isCooldown2 == false)
        {
            skillshot.GetComponent<Image>().enabled = false;
            indicatorRangeCirlce.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;
        }

        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown2 = true;
            skillImage2.fillAmount = 1;

            Vector3 newPosition = new Vector3(skill2canvas.transform.position.x, 10f, skill2canvas.transform.position.z);
            newtargetCircleArea = Instantiate(targetCircleArea, skill2canvas.transform.position, Quaternion.identity);
            disableTargetCircleTimer = 0f;
        }

        if (isCooldown2)
        {
            skillImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            indicatorRangeCirlce.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

            disableTargetCircleTimer += Time.deltaTime;

            if (disableTargetCircleTimer >= skill2Timer)
            {
                Destroy(newtargetCircleArea);
            }

            if (skillImage2.fillAmount <= 0)
            {
                skillImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }


    void Ultimate()
    {
        if (Input.GetKey(ultimate) && isCooldownUlt == false)
        {
            isCooldownUlt = true;
            ultimateImage.fillAmount = 1;
        }

        if (isCooldownUlt)
        {
            ultimateImage.fillAmount -= 1 / cooldownUlt * Time.deltaTime;

            if (ultimateImage.fillAmount <= 0)
            {
                ultimateImage.fillAmount = 0;
                isCooldownUlt = false;
            }
        }
    }
}

