using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer(MainMenu mainMenu)
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        PlayerData data = new PlayerData(mainMenu);
        string txt = JsonUtility.ToJson(data);
        File.WriteAllText(path, txt);
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        try
        {
            string content = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(content);
            return data;
        }
        catch
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}