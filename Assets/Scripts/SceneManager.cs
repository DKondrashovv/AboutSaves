using System.IO;
using Newtonsoft.Json;
using UnityEngine;


public class SceneManager : MonoBehaviour
{
    private int levelIndex;
    private LevelData _levelData;
    public void StartNewGame()
    {
        File.Delete(Application.persistentDataPath+ "/LevelDataConfig");
        File.Delete(Application.persistentDataPath+"/MoneyActiveData");
        File.Delete(Application.persistentDataPath+"/CurrentPosition");
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        if (File.Exists(Application.persistentDataPath + "/LevelDataConfig"))
        {
            var sd = File.ReadAllText(Application.persistentDataPath + "/LevelDataConfig");
            var newData = JsonConvert.DeserializeObject<LevelData>(sd);
            
            levelIndex = newData.levelIndex;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
}
