using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script is intended to allow the transitions between scenes, depending on a number of factors
/// Currently:
///     On Trigger Enter, I want to have the next scene load
/// </summary>
public class My_Scene_Manager : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int curentScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(curentScene % SceneManager.sceneCountInBuildSettings);
        }
    }

    public void LoadNextScene()
    {
        int curentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(curentScene % SceneManager.sceneCountInBuildSettings);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
