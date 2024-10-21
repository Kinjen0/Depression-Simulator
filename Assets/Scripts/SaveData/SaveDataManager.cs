using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// https://www.youtube.com/watch?v=aUi9aijvpgs&t=74s
public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance {  get; private set; }
    private GameData gameData;
    private List<IDataSaveInterface> dataSaveInterfaces;

    [SerializeField] private string fileName;
    private FileDataHandler fileDataHandler;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one save data manager");
        }
        Instance = this;
    }
    
    public int GetSavedLevel()
    {
        return gameData.savedlevel;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        this.dataSaveInterfaces = FindAllDataSaveInterfaces();
        //LoadGame();
        DontDestroyOnLoad(this);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        // Save the New GameData
        fileDataHandler.Save(this.gameData);
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        // Load any save data we have
        // If we dont actually have any game data, make a new one
        if(this.gameData == null)
        {
            Debug.Log("There was no data found, starting new game");
            NewGame();
        }
        this.dataSaveInterfaces = FindAllDataSaveInterfaces();

        foreach (IDataSaveInterface dataSaveInterface in dataSaveInterfaces)
        {
            dataSaveInterface.LoadData(gameData);
            Debug.Log("Loaded Something");
        }
        Debug.Log("Loaded level: " + gameData.savedlevel);
    }

    public void SaveGame()
    {
        this.dataSaveInterfaces = FindAllDataSaveInterfaces();

        foreach (IDataSaveInterface dataSaveInterface in dataSaveInterfaces)
        {
            dataSaveInterface.SaveData(ref gameData);
            Debug.Log("Saving Data");
        }
        fileDataHandler.Save(gameData);
    }

    // Theoretically save the game when we quit
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    // This function will let me manually save the game at certain points, such as when we load into a new level
    public void ForceSaveGame()
    {
        SaveGame();
    }

    

    private List<IDataSaveInterface> FindAllDataSaveInterfaces()
    {
        IEnumerable<IDataSaveInterface> dataSaveObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaveInterface>();
        return new List<IDataSaveInterface>(dataSaveObjects);
    }
}
