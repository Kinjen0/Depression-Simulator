using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=aUi9aijvpgs&t=74s
public interface IDataSaveInterface
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
