using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    GameObject player;
    playerControl playerCs;
    AudioSource audioSource;

    [SerializeField] AudioClip levelUpSound;

    public GameObject joystick;
    public GameObject movePlayer;
    public GameObject[] enemies, colletibles;
    public int exp, score, level, coinExp, highScore;
    public Vector3 spawnPos, returnPos;
    int enemyCount;

    float minute, second, LevelExp, coinLv;
    float spawnEnemyDelay, spawnEnemyRate, spawnColletiblesDelay, spawnCollectiblesRate;

    [SerializeField] GameObject lvlUpPanel, powerUpsPnl, startPnl, pausePnl, gameOverPnl, enemyParent;
    [SerializeField] TextMeshProUGUI lvTxt, scoreTxt, timeTxt, enemyCountTxt, gameOverScoreTxt, gameOverTimeTxt,highScoreTxt;
    [SerializeField] Image expImage, coinImage, enemyCountImage;
    [SerializeField] ParticleSystem levelUpParticle;
    [SerializeField] GameObject adBtn, asBtn, arBtn, healtBtn, speedBtn, magnetBtn;
    [SerializeField] AudioClip levelUpAudio;
    [SerializeField] Button pauseBtn;
   

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        Time.timeScale = 0;

        player = GameObject.Find("Player");
        playerCs = player.GetComponent<playerControl>();
        audioSource = player.GetComponent<AudioSource>();

        spawnEnemyDelay = 0.3f;
        spawnEnemyRate = 2f;
        spawnColletiblesDelay = 15f;
        spawnCollectiblesRate = 30f;

        coinExp = 0;
        coinLv = 200;
        minute = 0;
        second = 0;
        score = 0;
        LevelExp = 100;
        exp = 0;
        level = 1;
        enemyCount = playerCs.Enemies.Length + 1;
        scoreTxt.text = "SCORE  :" + score;
        lvTxt.text = "Lv " + level;
        enemyCountTxt.text = "% " + playerCs.Enemies.Length;

        expImage.rectTransform.localScale = new Vector3((float)exp / LevelExp, 1, 1);
        coinImage.rectTransform.localScale = new Vector3((float)coinExp / coinLv, 1, 1);
        enemyCountImage.rectTransform.localScale = new Vector3(enemyCount / 100, 1, 1);

        joystick.SetActive(false);
        lvlUpPanel.SetActive(false);
        powerUpsPnl.SetActive(false);
        pausePnl.SetActive(false);
        gameOverPnl.SetActive(false);
        startPnl.SetActive(true);
        pauseBtn.interactable = false;

        InvokeRepeating("SpawnEnemy", spawnEnemyDelay, spawnEnemyRate);
        InvokeRepeating("SpawnCollectibles", spawnColletiblesDelay, spawnCollectiblesRate);
    }

    void Update()
    {
        enemyCount = playerCs.Enemies.Length;
        scoreTxt.text = "SCORE : " + score;
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        enemyCountTxt.text = "% " + enemyCount;

        coinImage.rectTransform.localScale = new Vector3((float)coinExp / coinLv, 1, 1);
        expImage.rectTransform.localScale = new Vector3((float)exp / LevelExp, 1, 1);
        enemyCountImage.rectTransform.localScale = new Vector3((float)enemyCount / 100, 1, 1);


        if (exp >= LevelExp)
        {
            LevelUp();
        }
        if (coinExp >= coinLv)
        {
            Time.timeScale = 0;
            joystick.SetActive(false);
            powerUpsPnl.SetActive(true);
            coinExp = 0;
            coinLv *= 3.2f;
        }

        if (playerCs.healt <= 0)
        {
            playerCs.healt = 0;
            GameOver();
        }

        if (enemyCount >= 100)
        {
            enemyCount = 100;
            GameOver();
        }

        Timer();
    }

    void SpawnEnemy()
    {
        if (level <= 3)
        {
            spawnPos = SpawnPos();
            GameObject obj = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation) as GameObject;
            obj.transform.SetParent(enemyParent.transform);
        }
        else if (level > 3 && level <= 8)
        {
            spawnPos = SpawnPos();
            float enemyLevel = Random.Range(0, 15);
            if (enemyLevel < 5)
            {
                GameObject obj = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation) as GameObject;
                obj.transform.SetParent(enemyParent.transform);
            }
            else
            {
                GameObject obj = Instantiate(enemies[1], spawnPos, enemies[1].transform.rotation) as GameObject;
                obj.transform.SetParent(enemyParent.transform);
            }
        }
        else if (level > 8 && level <= 13)
        {
            float enemyCount = 2;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[1], spawnPos, enemies[1].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 17)
                {
                    GameObject obj = Instantiate(enemies[2], spawnPos, enemies[2].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[3], spawnPos, enemies[3].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 13 && level <= 17)
        {
            float enemyCount = 3;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[1], spawnPos, enemies[1].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[2], spawnPos, enemies[2].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 17)
                {
                    GameObject obj = Instantiate(enemies[3], spawnPos, enemies[3].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[4], spawnPos, enemies[4].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 17 && level <= 21)
        {
            float enemyCount = 4;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[2], spawnPos, enemies[2].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[3], spawnPos, enemies[3].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 17)
                {
                    GameObject obj = Instantiate(enemies[4], spawnPos, enemies[4].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[5], spawnPos, enemies[5].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 21 && level <= 25)
        {
            float enemyCount = 5;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[3], spawnPos, enemies[3].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[4], spawnPos, enemies[4].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 17)
                {
                    GameObject obj = Instantiate(enemies[5], spawnPos, enemies[5].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[6], spawnPos, enemies[6].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 25 && level <= 28)
        {
            float enemyCount = 6;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[4], spawnPos, enemies[4].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[5], spawnPos, enemies[5].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 17)
                {
                    GameObject obj = Instantiate(enemies[6], spawnPos, enemies[6].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[7], spawnPos, enemies[7].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 28 && level <= 30)
        {
            float enemyCount = 7;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[5], spawnPos, enemies[5].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[6], spawnPos, enemies[6].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 17)
                {
                    GameObject obj = Instantiate(enemies[7], spawnPos, enemies[7].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[8], spawnPos, enemies[8].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 30 && level <= 32)
        {
            float enemyCount = 10;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[7], spawnPos, enemies[7].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[8], spawnPos, enemies[8].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[9], spawnPos, enemies[9].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 32 && level <= 33) 
        {
            float enemyCount = 13;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[7], spawnPos, enemies[7].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[8], spawnPos, enemies[8].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[9], spawnPos, enemies[9].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
         else if (level > 33 && level <= 35) 
        {
            float enemyCount = 15;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[7], spawnPos, enemies[7].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[8], spawnPos, enemies[8].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[9], spawnPos, enemies[9].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }
        else if (level > 35 && level <= 40)
        {
            float enemyCount = 20;
            for (int i = 0; i <= enemyCount; i++)
            {
                spawnPos = SpawnPos();
                float enemyLevel = Random.Range(0, 20);
                if (enemyLevel < 2)
                {
                    GameObject obj = Instantiate(enemies[7], spawnPos, enemies[7].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else if (enemyLevel < 7)
                {
                    GameObject obj = Instantiate(enemies[8], spawnPos, enemies[8].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
                else
                {
                    GameObject obj = Instantiate(enemies[9], spawnPos, enemies[9].transform.rotation) as GameObject;
                    obj.transform.SetParent(enemyParent.transform);
                }
            }
        }

    }

    void SpawnCollectibles()
    {
        spawnPos = new Vector3(movePlayer.transform.position.x + Random.Range(-10, 10), 1, movePlayer.transform.position.z + Random.Range(-10, 10));
        float index = Random.Range(0, 5);
        if (index <= 2)
        {
            Instantiate(colletibles[0], spawnPos, colletibles[0].transform.rotation);
        }
        else
        {
            Instantiate(colletibles[1], spawnPos, colletibles[1].transform.rotation);
        }
    }

    Vector3 SpawnPos()
    {
        for (int i = 0; i < 200; i++)
        {
            returnPos = new Vector3(movePlayer.transform.position.x + Random.Range(-40, 40), 1f, movePlayer.transform.position.z + Random.Range(-40, 40));
            float distance = Vector3.Distance(returnPos, movePlayer.transform.position);
            if (distance >= 30)
            {
                break;
            }
        }
        return (returnPos);
    }

    public void LevelUp()
    {
        if (level >= 40)
        {
            exp = (int)LevelExp;
            lvTxt.text = "Max Lv";
        }
        audioSource.PlayOneShot(levelUpAudio);
        levelUpParticle.Play();
        Time.timeScale = 0;
        level = level + 1;
        LevelExp *= 1.35f;
        lvTxt.text = "Lv " + level;
        exp = 0;
        playerCs.attackSpeed -= (playerCs.attackSpeed / 100) * 4;
        playerCs.attackDmg += (playerCs.attackDmg / 100) * 5f;
        playerCs.speed += (playerCs.speed / 100) * 1;
        playerCs.attackRange += (playerCs.attackRange / 100) * 2;
        playerCs.maxHealt += (playerCs.maxHealt / 100) * 5;
        playerCs.magnetDistanece += (playerCs.magnetDistanece / 100) * 2;
        playerCs.healt += (playerCs.healt / 100) * 5;
        joystick.SetActive(false);
        lvlUpPanel.SetActive(true);
        ChangeButton();
    }

    void GameOver()
    {
        joystick.SetActive(false);
        Time.timeScale = 0;
        gameOverPnl.SetActive(true);
        pauseBtn.interactable = false;
        gameOverScoreTxt.text = scoreTxt.text;
        gameOverTimeTxt.text = timeTxt.text;
        highScoreTxt.text = "HIGH SCORE : " + highScore;
    }

    void Timer()
    {
        second += Time.deltaTime;
        if (second >= 59)
        {
            minute += 1;
            second = 0;
        }
        if (minute < 9)
        {
            if (second < 10)
            {
                timeTxt.text = "TIME : " + "0" + (int)minute + " : " + "0" + (int)second;
            }
            else
            {
                timeTxt.text = "TIME : " + "0" + (int)minute + " : " + (int)second;
            }
        }
        else
        {
            if (second < 10)
            {
                timeTxt.text = "TIME : " +  (int)minute + " : " + "0" + (int)second;
            }
            else
            {
                timeTxt.text = "TIME : " +  (int)minute + " : " + (int)second;
            }
        }
    }

    void ChangeButton()
    {
        int random1, random2,random3;
        random1 = Random.Range(0, 2);
        random2 = Random.Range(0, 2);
        random3 = Random.Range(0, 2);
        if (random1 == 0)
        {
            adBtn.SetActive(true);
            healtBtn.SetActive(false);
        }
        else
        {
            adBtn.SetActive(false);
            healtBtn.SetActive(true);
        }
        if (random2 == 0)
        {
            asBtn.SetActive(true);
            speedBtn.SetActive(false);
        }
        else
        {
            asBtn.SetActive(false);
            speedBtn.SetActive(true);
        }
        if (random1 == 0)
        {
            arBtn.SetActive(true);
            magnetBtn.SetActive(false);
        }
        else
        {
            arBtn.SetActive(false);
            magnetBtn.SetActive(true);
        }

    }

    public void StartGame()
    {
        joystick.SetActive(true);
        Time.timeScale = 1;
        startPnl.SetActive(false);
        pauseBtn.interactable = true;
    }

    public void Continue()
    {
        joystick.SetActive(true);
        Time.timeScale = 1;
        pausePnl.SetActive(false);
        pauseBtn.interactable = true;
    }

    public void Pause()
    {
        joystick.SetActive(false);
        Time.timeScale = 0;
        pauseBtn.interactable = false;
        pausePnl.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
