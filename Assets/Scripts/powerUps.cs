using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class powerUps : MonoBehaviour
{
    public GameObject joystick;
    public bool multiShot;
    public int adLv, asLv, arLv, speedLv, healtLv, magnetLv, bounceShotLv, regenerationLv;
    public ParticleSystem healtParticle;
    float regeneration,regenerationRate;
    

    [SerializeField] TextMeshProUGUI adTxt, asTxt, arTxt, speedTxt, healtTxt, magnetTxt, bounceTxt, regenerationTxt;
    [SerializeField] Button adBtn, asBtn, arBtn, speedBtn, healtBtn, magnetBtn, bounceBtn, multiShotBtn, regenerationBtn;
    [SerializeField] GameObject lvlUpPanel, powerUpsPanel;

    playerControl playerCs;
    gameManager gM;

    void Start()
    {
        playerCs = GameObject.Find("Player").GetComponent<playerControl>();
        gM = GetComponent<gameManager>();

        adLv = 1;
        asLv = 1;
        arLv = 1;
        speedLv = 1;
        healtLv = 1;
        magnetLv = 1;
        bounceShotLv = 1;
        regeneration = 0;
        regenerationRate = 10;

        adTxt.text = "Lv " + adLv;
        asTxt.text = "Lv " + asLv;
        arTxt.text = "Lv " + arLv;
        speedTxt.text = "Lv " + speedLv;
        healtTxt.text = "Lv " + healtLv;
        magnetTxt.text = "Lv " + magnetLv;
        bounceTxt.text = "Lv " + bounceShotLv;

        multiShot = false;

    }
    void Update()
    {
        regenerationRate -= Time.deltaTime;
        if (regenerationRate <= 0 && playerCs.healt < playerCs.maxHealt && regeneration > 0) 
        {
            playerCs.healt += regeneration;
            healtParticle.Play();
            regenerationRate = 10;
        }
        if (adLv >= 10)
        {
            adBtn.interactable = false;
        }
        if (asLv >= 10)
        {
            asBtn.interactable = false;
        }
        if (arLv >= 10)
        {
            arBtn.interactable = false;
        }
        if (speedLv >= 10)
        {
            speedBtn.interactable = false;
        }
        if (healtLv >= 10)
        {
            healtBtn.interactable = false;
        }
        if (magnetLv >= 10)
        {
            magnetBtn.interactable = false;
        }
        if (bounceShotLv >= 10)
        {
            bounceBtn.interactable = false;
        }
        if (regenerationLv >= 10)
        {
            regenerationBtn.interactable = false;
        }
    }
    public void AttackDamage()
    {
        playerCs.attackDmg += (playerCs.attackDmg / 100) * 10;
        adLv += 1;
        adTxt.text = "Lv " + adLv;
        joystick.SetActive(true);
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;
        
    }
    public void AttackSpeed()
    {
        playerCs.attackSpeed -= (playerCs.attackSpeed / 100) * 10;
        asLv += 1;
        asTxt.text = "Lv " + asLv;
        joystick.SetActive(true);
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void AttackRange()
    {
        playerCs.attackRange += (playerCs.attackRange / 100) * 5;
        arLv += 1;
        arTxt.text = "Lv " + arLv;
        joystick.SetActive(true);
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Speed()
    {
        playerCs.speed += (playerCs.attackSpeed / 100) * 5;
        speedLv += 1;
        speedTxt.text = "Lv " + speedLv;
        joystick.SetActive(true);
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Healt()
    {
        playerCs.maxHealt += (playerCs.maxHealt / 100) * 10;
        playerCs.healt += (playerCs.healt / 100) * 10;
        healtLv += 1;
        healtTxt.text = "Lv " + healtLv;
        joystick.SetActive(true);
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void MagnetDistance()
    {
        playerCs.magnetDistanece += (playerCs.magnetDistanece / 100) * 10;
        magnetLv += 1;
        magnetTxt.text = "Lv " + magnetLv;
        joystick.SetActive(true);
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void BounceShot()
    {
        bounceShotLv += 1;
        bounceTxt.text = "Lv " + bounceShotLv;
        joystick.SetActive(true);
        powerUpsPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void MultiShot()
    {
        multiShot = true;
        multiShotBtn.interactable = false;
        joystick.SetActive(true);
        powerUpsPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Regeneration()
    {
        regeneration += 5;
        regenerationLv += 1;
        regenerationTxt.text = "Lv " + regenerationLv;
        joystick.SetActive(true);
        powerUpsPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
