using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData)
    {
        string JSON = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, JSON);
    }

    public static T LoadData<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string JSON = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(JSON);
        }
        else
            return new T();
    }
}