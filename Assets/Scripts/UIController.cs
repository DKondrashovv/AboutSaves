using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private Text healtText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject teleportPanel;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private List<GameObject> moneyOnScene;
    [SerializeField] private MoneySpawner _moneySpawner;
    private LevelData myData;
    public List<bool> activeMoney;

    private int levelIndex;
    private int money;
    private int health=100;
    
    private void OnEnable()
    {
        MoneyController.OnMoneySet += ViewMoney;
        PlayerController.OnWallCollision += ViewHealth;
    }

    private void Awake()
    {
        levelIndex = _levelConfig.LevelIndex;
        moneyOnScene = _moneySpawner.money;
        if (File.Exists(Application.persistentDataPath + "/LevelDataConfig"))
        {
             var sd = File.ReadAllText(Application.persistentDataPath + "/LevelDataConfig");
             var newData = JsonConvert.DeserializeObject<LevelData>(sd);
             levelIndex = myData.GetLevel(_levelConfig);
             money = newData.money;
             health = newData.health;
        }
    }

    private void OnDisable()
    {
        MoneyController.OnMoneySet -= ViewMoney;
        PlayerController.OnWallCollision -= ViewHealth;
    }

    private void ViewHealth(int _health)
    {
        health = _health;
        healtText.text = $"Health : {health.ToString()}";
    }

    private void Start()
    {
        healtText.text = $"Health : {health}";
        levelText.text = $"Level : {levelIndex}";
        moneyText.text = $"Money : {money}";
        teleportPanel.SetActive(false);
    }


    private void ViewMoney()
    {
        money++;
        moneyText.text = $"Money : {money.ToString()}";
        if (money == 5)
        {
            teleportPanel.SetActive(true);
        }
    }
    public void SavePlayerData()
    {
        myData.money = money;
        myData.health = health;
        myData.levelIndex = myData.GetLevel(_levelConfig);
        myData.playerPosition = myData.GetPlayerPosition(_levelConfig);
        var myData1 = JsonUtility.ToJson(myData);
        File.WriteAllText(Application.persistentDataPath+ "/LevelDataConfig",myData1);
    }
    

    public void SaveLevelData()
    {
        activeMoney = new List<bool>();
        foreach (var money in moneyOnScene)
        {
            activeMoney.Add(money.activeInHierarchy);
        }
        var serializeMoneyData = JsonConvert.SerializeObject(activeMoney);
        File.WriteAllText( Application.persistentDataPath+"/MoneyActiveData",serializeMoneyData);
    }

    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/LevelDataConfig"))
        {
            var sd = File.ReadAllText(Application.persistentDataPath + "/LevelDataConfig");
            var newData = JsonConvert.DeserializeObject<LevelData>(sd);
            levelIndex = newData.GetLevel(_levelConfig);
            money = newData.money;
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
        }
    }
    
    public void LoadLevelData()
    {
        if (File.Exists(Application.persistentDataPath + "/MoneyActiveData"))
        {
            var unActiveMoney = File.ReadAllText(Application.persistentDataPath + "/MoneyActiveData");
            var newData1 = JsonConvert.DeserializeObject<List<bool>>(unActiveMoney);
            for (int i = 0; i < moneyOnScene.Count; i++)
            {
                moneyOnScene[i].SetActive(newData1[i]);
            }
        }
    }

}
