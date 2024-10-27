using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script is intended to allow the transitions between scenes, depending on a number of factors
/// Currently:
///     On Trigger Enter, I want to have the next scene load
/// Additionally, as it is used throughout my game to load the next scene, it is perfect to save the curent level
///     
/// </summary>
public class My_Scene_Manager : MonoBehaviour, IDataSaveInterface
{

    private int curentScene = 0;

    private void Start()
    {
        if (SaveDataManager.Instance != null)
        {
            SaveDataManager.Instance.LoadGame();
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SaveDataManager.Instance.ForceSaveGame();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            curentScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(curentScene % SceneManager.sceneCountInBuildSettings);
        }
    }

    public void LoadNextScene()
    {
        curentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(curentScene % SceneManager.sceneCountInBuildSettings);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadData(GameData data)
    {
        this.curentScene = data.savedlevel;
    }

    public void StartNewGame()
    {
        SaveDataManager.Instance.NewGame();
        LoadNextScene();
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(SaveDataManager.Instance.GetSavedLevel());
    }

    public void SaveData(ref GameData data)
    {
        if(data == null)
        {
            Debug.LogError("For some reason the data is null");
        }
        data.savedlevel = SceneManager.GetActiveScene().buildIndex;
    }
}
