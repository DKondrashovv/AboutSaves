using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
   [SerializeField] private UIController _uiController;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _uiController.SavePlayerData();
            _uiController.SaveLevelData();
            File.Delete(Application.persistentDataPath+ "/MoneyActiveData");
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}
