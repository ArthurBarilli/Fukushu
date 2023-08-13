using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameManager>();
            }
            return _Instance;
        }
    }
    
    public GameObject player;
    public Transform spawn;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera vCamera;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> projectiles = new List<GameObject>();
    public GameObject gameOverScreen;

    [Header("Tutorial")]
    public bool isTAttacking;
    public bool isTSlowed;

    //skilltree bools

    [Header("Skill tree")]
    public bool isChargeAttack;
    public bool isDashUpgrade;
    public bool isSuperDash;
    public bool isParrySlow;
    public bool isParryProjectile;
    public bool ultimatePower;
    public SkillsManager currentStore;

    //slowtimeRefs
    [Header("Slow Time")]
    public bool isSlowed;
    public float timeSpeed;
    public float normalSpeed = 1;
    public float slowDuration = 3;
    public float timerTime = 1;
    public float slowedTime;

    [Header("Efeitos Camera")]
    public Volume effect;
    public float defaultLensDistortion;
    public float slowLenDistortion;
    public float defaultChromatic;
    public float slowChromatic;
    //Scenes
    public Scene currentScene;
    public Scene nextScene;
    public string nextSceneName;
    //tutorial
    public bool tutorial;

    // points
    [Header("Points Var")]
    public float points;
    public float levelPoints;
    
    public float time;
    public bool stopped;

    public float levelSTime;
    public float levelATime;
    public float levelBTime;
    public float levelCTime;
    public float levelDTime;
    public float levelFTime;
    public char LevelRank;
    public int diedTimes;

    [Header("UIs")]
    public GameObject UILevelEnd;
    public TMP_Text levelRankText;
    public TMP_Text timeUI;
    public TMP_Text currentPointsText;
    public TMP_Text levelEndTimer;
    public TMP_Text triesText;
    public int enemiesKilled;
    public Sprite enemyBow;
    public Sprite enemySword;
    public Sprite enemyShield;
    public Sprite Boss;
    public Sprite blankImage;
    public Image[] enemiesKilledPlace;
    public bool levelEnd;

    //tutorial vars


    

    private void Awake()
    {

        if (_Instance == null)
        {
            _Instance = this;
        }
        else 
        {
            Destroy (gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentScene = SceneManager.GetActiveScene();
        nextSceneName = "StoreScene1";
        
    }
    private void Update() 
    {
        DontDestroyOnLoad(this.gameObject);   
        if(stopped == false){
            time += Time.deltaTime * timerTime - slowedTime;
        }
        timeUI.text = time.ToString("0.##");
    }


    //qual level esta sendo carregado
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "Tutorial":
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            effect.enabled = false;
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            player = GameObject.FindGameObjectWithTag("Player");
            vCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
            spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
            time = 0;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "StoreScene1";
            levelSTime = 300;
            levelATime = 301;
            levelBTime = 302;
            levelCTime = 303;
            levelDTime = 304;
            levelFTime = 305;
            Debug.Log("Tutorial");
            for (int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i]);
                switch(enemies[i].GetComponent<EnemyLife>().enemyType)
                {
                    case "Bow":
                    enemiesKilledPlace[i].sprite = enemyBow;
                    break;
                    case "Sword":
                    enemiesKilledPlace[i].sprite = enemySword;
                    break;
                    case "Shield":
                    enemiesKilledPlace[i].sprite = enemyShield;
                    break;
                    case"Porrete":
                    enemiesKilledPlace[i].sprite = Boss;
                    break;
                }
            }
            break;


            case "StoreScene1":
            time = 0;
            diedTimes = 0;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "Level 1";
            effect.enabled = false;
            Debug.Log("Loja 1");
            break;


            case "Level 1":
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            effect.enabled = false;
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            player = GameObject.FindGameObjectWithTag("Player");
            vCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
            time = 0;
            stopped = false;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "StoreScene2";
            levelSTime = 45;
            levelATime = 50;
            levelBTime = 55;
            levelCTime = 60;
            levelDTime = 65;
            levelFTime = 70;
            Debug.Log("Level 1");
            for (int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i]);
                switch(enemies[i].GetComponent<EnemyLife>().enemyType)
                {
                    case "Bow":
                    enemiesKilledPlace[i].sprite = enemyBow;
                    break;
                    case "Sword":
                    enemiesKilledPlace[i].sprite = enemySword;
                    break;
                    case "Shield":
                    enemiesKilledPlace[i].sprite = enemyShield;
                    break;
                    case"Porrete":
                    enemiesKilledPlace[i].sprite = Boss;
                    break;
                }
            }
            break;

            case "StoreScene2":
            time = 0;
            diedTimes = 0;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "Level 2";
            effect.enabled = false;
            Debug.Log("Loja 2");
            currentStore = FindObjectOfType<SkillsManager>();
            currentStore.dash2 = isDashUpgrade;
            currentStore.superDash = isSuperDash;
            currentStore.chargeAttack = isChargeAttack;
            currentStore.parryProjectile = isParryProjectile;
            currentStore.parrySlowTime = isParrySlow;
            break;

            case "Level 2":
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            effect.enabled = false;
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            player = GameObject.FindGameObjectWithTag("Player");
            vCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
            time = 0;
            stopped = false;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "BossKill";
            levelSTime = 80;
            levelATime = 85;
            levelBTime = 90;
            levelCTime = 95;
            levelDTime = 100;
            levelFTime = 105;
            Debug.Log("Level 2");
            for (int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i]);
                switch(enemies[i].GetComponent<EnemyLife>().enemyType)
                {
                    case "Bow":
                    enemiesKilledPlace[i].sprite = enemyBow;
                    break;
                    case "Sword":
                    enemiesKilledPlace[i].sprite = enemySword;
                    break;
                    case "Shield":
                    enemiesKilledPlace[i].sprite = enemyShield;
                    break;
                    case"Porrete":
                    enemiesKilledPlace[i].sprite = Boss;
                    break;
                }
            }
            break;
            
            case "StoreScene3":
            time = 0;
            diedTimes = 0;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "Level 3";
            effect.enabled = false;
            Debug.Log("Loja 3");
            currentStore = FindObjectOfType<SkillsManager>();
            currentStore.dash2 = isDashUpgrade;
            currentStore.superDash = isSuperDash;
            currentStore.chargeAttack = isChargeAttack;
            currentStore.parryProjectile = isParryProjectile;
            currentStore.parrySlowTime = isParrySlow;
            break;


            case "Level 3":
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            effect.enabled = false;
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            player = GameObject.FindGameObjectWithTag("Player");
            time = 0;
            stopped = false;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "StoreScene4";
            levelSTime = 60;
            levelATime = 65;
            levelBTime = 70;
            levelCTime = 75;
            levelDTime = 80;
            levelFTime = 85;
            Debug.Log("Level 3");
            for (int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i]);
                switch(enemies[i].GetComponent<EnemyLife>().enemyType)
                {
                    case "Bow":
                    enemiesKilledPlace[i].sprite = enemyBow;
                    break;
                    case "Sword":
                    enemiesKilledPlace[i].sprite = enemySword;
                    break;
                    case "Shield":
                    enemiesKilledPlace[i].sprite = enemyShield;
                    break;
                    case"Porrete":
                    enemiesKilledPlace[i].sprite = Boss;
                    break;
                }
            }
            break;
            
            case "StoreScene4":
            time = 0;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "Level 4";
            effect.enabled = false;
            Debug.Log("Loja 4");
            currentStore = FindObjectOfType<SkillsManager>();
            currentStore.dash2 = isDashUpgrade;
            currentStore.superDash = isSuperDash;
            currentStore.chargeAttack = isChargeAttack;
            currentStore.parryProjectile = isParryProjectile;
            currentStore.parrySlowTime = isParrySlow;
            break;

            case "Level 4":
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            effect.enabled = false;
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            player = GameObject.FindGameObjectWithTag("Player");
            time = 0;
            stopped = false;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "StoreScene5";
            levelSTime = 160;
            levelATime = 165;
            levelBTime = 170;
            levelCTime = 175;
            levelDTime = 180;
            levelFTime = 185;
            Debug.Log("Level 4");
            for (int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i]);
                switch(enemies[i].GetComponent<EnemyLife>().enemyType)
                {
                    case "Bow":
                    enemiesKilledPlace[i].sprite = enemyBow;
                    break;
                    case "Sword":
                    enemiesKilledPlace[i].sprite = enemySword;
                    break;
                    case "Shield":
                    enemiesKilledPlace[i].sprite = enemyShield;
                    break;
                    case"Porrete":
                    enemiesKilledPlace[i].sprite = Boss;
                    break;
                }
            }
            break;

            case "StoreScene5":
            time = 0;
            diedTimes = 0;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "Level 5";
            effect.enabled = false;
            Debug.Log("Loja 5");
            currentStore = FindObjectOfType<SkillsManager>();
            currentStore.dash2 = isDashUpgrade;
            currentStore.superDash = isSuperDash;
            currentStore.chargeAttack = isChargeAttack;
            currentStore.parryProjectile = isParryProjectile;
            currentStore.parrySlowTime = isParrySlow;
            break;


            case "Level 5":
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            effect.enabled = false;
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            player = GameObject.FindGameObjectWithTag("Player");
            time = 0;
            stopped = false;
            currentScene = SceneManager.GetActiveScene();
            nextSceneName = "FinalCutscene1";
            levelSTime = 300;
            levelATime = 301;
            levelBTime = 302;
            levelCTime = 303;
            levelDTime = 304;
            levelFTime = 305;
            Debug.Log("Level 5");
            for (int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i]);
                switch(enemies[i].GetComponent<EnemyLife>().enemyType)
                {
                    case "Bow":
                    enemiesKilledPlace[i].sprite = enemyBow;
                    break;
                    case "Sword":
                    enemiesKilledPlace[i].sprite = enemySword;
                    break;
                    case "Shield":
                    enemiesKilledPlace[i].sprite = enemyShield;
                    break;
                    case"Porrete":
                    enemiesKilledPlace[i].sprite = Boss;
                    break;
                }
            }
            break;
        }
    }

    //funcoes a serem chamadas pelo instance
    public void NextLevel()
    {
       StartCoroutine(StartLevel());
    }
    public void Die()
    {
        Time.timeScale = 0.1f;
        gameOverScreen.SetActive(true);
    }

    public void AddPoints(float points)
    {
        levelPoints += points;
    }


    //calculador de pontos 
    IEnumerator EndLevel()
    {
        if(levelEnd == false)
        {
            levelEnd = true;
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(3);
            Time.timeScale = 1;
            UILevelEnd.SetActive(true);
            stopped = true;
            //multiplicador positivo de tempo
            if (time > levelDTime)
            {
                LevelRank = 'F';
            }
            else if (time <= levelDTime && time > levelCTime)
            {
                LevelRank = 'D';
                levelPoints = levelPoints * 1.2f;
            }
            else if (time <= levelCTime && time > levelBTime)
            {
                LevelRank = 'C';
                levelPoints = levelPoints * 1.4f;
            }
            else if (time <= levelBTime && time > levelATime)
            {
                LevelRank = 'B';
                levelPoints = levelPoints * 1.6f;
            }
            else if (time <= levelATime && time > levelSTime)
            {
                LevelRank = 'A';
                levelPoints = levelPoints * 1.8f;
            }
            else if (time < levelSTime)
            {
                LevelRank = 'S';
                levelPoints = levelPoints * 2f;
            }
            // multiplicador negativo de x vezes morto
            switch(diedTimes)
            {
                case 1:
                levelPoints = levelPoints * 0.975f;
                break;
                case 2:
                levelPoints = levelPoints * 0.950f;
                break;
                case 3:
                levelPoints = levelPoints * 0.925f;
                break;
                case 4:
                levelPoints = levelPoints * 0.8f;
                break;
                case > 4:
                levelPoints = levelPoints * 0.875f;
                break;
            }
            levelPoints = Mathf.RoundToInt(levelPoints);
            currentPointsText.text = levelPoints.ToString();
            levelRankText.text = LevelRank.ToString();
            levelEndTimer.text = time.ToString("0.##");
            diedTimes++;
            triesText.text = diedTimes.ToString();
            foreach (var item in enemies)
            {
                item.GetComponent<EnemyAI>().speed = 0.1f;
                item.GetComponent<Animator>().SetFloat("Time", 0.1f);
            }
        }
    }


    public void ResetLevel()
    {
        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }


    //camera lenta
    public void SlowDownTime()
    {
        isSlowed = true;
        effect.enabled = true;
        player.GetComponent<PlayerCombat>().slowed = true;
        player.GetComponent<SandevistanEffect>().slowed = true;
        projectiles.AddRange(GameObject.FindGameObjectsWithTag("Projectile"));
        timerTime = timeSpeed;
        if(projectiles.Count > 0)
        {
        foreach (var item in projectiles)
        {
            if (item.GetComponent<Arrow>() != null) item.GetComponent<Arrow>().timeSpeed = timeSpeed;
            else if (item.GetComponent<ShockWave>() != null)item.GetComponent<ShockWave>().speed = timeSpeed; 
        }
        }
        foreach (var item in enemies)
        {
            item.GetComponent<Animator>().SetFloat("Time",timeSpeed);
            item.GetComponent<EnemyAI>().speed = timeSpeed;
        }
        StartCoroutine(SlowingTime(slowDuration));
        //StartCoroutine(SlowTimeEffect(0.1f));
    }

    //hitstop
    public void HitStopTrigger(float intensity)
    {
         // float hitStopTime = 0;
         // Time.timeScale = 0;
         // while (hitStopTime < intensity)
         // {
         //     hitStopTime += Time.unscaledDeltaTime;
         // }
         // Time.timeScale = 1;
         // hitStopTime = 0;
         Time.timeScale = 0.1f;
         Time.fixedDeltaTime = Time.timeScale * 0.02f; // Adjust the physics update rate to match the time scale
         float endTime = Time.realtimeSinceStartup + intensity;
         while (Time.realtimeSinceStartup < endTime)
         {
             // Wait for the hit-stop duration to elapse
         }
         Time.timeScale = 1;
         Time.fixedDeltaTime = 0.02f;
     }

    //remover da lista
    public void RemoveFromList(GameObject removed)
    {
        projectiles.Remove(removed);
    }
    public void RemoveEnemyFromList(GameObject removed)
    {
        enemies.Remove(removed);
        if (enemies.Count == 0)
        {
            StartCoroutine(EndLevel());
        }
    }
    public void CallLevelEnd()
    {
        StartCoroutine(EndLevel());
    }

    public void GoTo(string scene)
    {
        enemies.Clear();
        SceneManager.LoadScene(scene);
    }
    
    public void Respawn()
    {
        enemies.Clear();
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.name);
        levelPoints = 0;
        diedTimes ++;
    }
    // coroutines abaixo


    // public IEnumerator SlowTimeEffect(float duration)
    // {
    //     float tempo = 0f;
    //     while (tempo < duration)
    //     {
    //         if (effect.profile.TryGet<ChromaticAberration>(out ChromaticAberration cromatic))
    //         {
    //             cromatic.intensity = 1;
    //         }

    //         Mathf.Lerp(0f, slowChromatic, tempo / duration);
            
    //         tempo += Time.deltaTime;
    //         yield return null;
    //     }
    // }

    public IEnumerator StartLevel()
    {
        SceneManager.LoadScene(nextSceneName);
        UILevelEnd.SetActive(false);
        points += levelPoints;
        levelPoints = 0;
        foreach (Image im in enemiesKilledPlace)
        {
            im.sprite = blankImage;
        }
        levelEnd = false;
        yield return null;
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(currentScene.buildIndex+1);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + progress);

            yield return null;
        }
        nextScene = SceneManager.GetSceneByName(nextSceneName);
        Debug.Log(nextScene);
        Debug.Log(currentScene);
    }



    IEnumerator SlowingTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        timerTime = normalSpeed;
        effect.enabled =false;
        isSlowed = false;
        player.GetComponent<PlayerCombat>().slowed = false;
        player.GetComponent<SandevistanEffect>().slowed = false;
        if(projectiles.Count > 0)
        {
        foreach (var item in projectiles)
        {
            if (item.GetComponent<Arrow>() != null)item.GetComponent<Arrow>().timeSpeed = normalSpeed; 
            else if (item.GetComponent<ShockWave>() != null)item.GetComponent<ShockWave>().speed = normalSpeed; 
        }
        }
        foreach (var item in enemies)
        {
            item.GetComponent<Animator>().SetFloat("Time",normalSpeed);
            item.GetComponent<EnemyAI>().speed = normalSpeed;
        }
    }


}