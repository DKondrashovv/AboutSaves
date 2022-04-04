using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public struct LevelData
{
    private LevelConfig _levelConfig;
    public int money;
    public int health;
    public int levelIndex;
    public Vector3 playerPosition;
    
    public int GetLevel(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        return _levelConfig.LevelIndex;
    }

    public Vector3 GetPlayerPosition(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        return _levelConfig.PlayerPosition;
    }
}

public class LevelController : MonoBehaviour
{
    
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LevelConfig _levelConfig;
    private LevelData myData;

    private void Start()
    {
        var newPlayerPosition = myData.GetPlayerPosition(_levelConfig);
        Instantiate(playerPrefab, newPlayerPosition, Quaternion.identity);
    }
}
