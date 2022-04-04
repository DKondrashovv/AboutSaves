using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;
    public List<GameObject> money;
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var moneys = Instantiate(moneyPrefab, new Vector3(2 * i, 2, 2 * i + 3 * i), Quaternion.identity, transform);
            money.Add(moneys);
        }

        if (File.Exists(Application.persistentDataPath + "/MoneyActiveData"))
        {
            var unActiveMoney = File.ReadAllText(Application.persistentDataPath + "/MoneyActiveData");
            var newData1 = JsonConvert.DeserializeObject<List<bool>>(unActiveMoney);
            for (int i = 0; i < newData1.Count; i++)
            {
                money[i].SetActive(newData1[i]);
            }
        }
    }
    
}
