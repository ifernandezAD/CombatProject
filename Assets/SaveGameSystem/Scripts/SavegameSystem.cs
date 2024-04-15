using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SavegameSystem : MonoBehaviour
{
    [System.Serializable]
    class Record
    {
        public enum Type
        {
            String,
            Int,
            Float,
            Bool,
        }

        public string key;

        public Type type;
        public string stringValue;
        public int intValue;
        public float floatValue;
        public bool boolValue;
    };


    static Dictionary<string, Record> currentSaveGame;

    static Dictionary<string, Record>[] saveGames = new Dictionary<string, Record>[] { new(), new(), new() };

    //Habrá que generar otra funcion para int,float y bool
    public static string GetString(string key, string defaultValue = "")
    {
        string retVal = defaultValue;
        if (currentSaveGame.TryGetValue(key, out Record record))
        {
            AssertRecordType(key, record, Record.Type.String);
            retVal = record.stringValue;
        }
        return retVal;
    }

    public static void SetString(string key, string newValue)
    {
        Record record;
        if (!currentSaveGame.TryGetValue(key, out record))
        {
            record = new();
            record.key = key;
            record.type = Record.Type.String;
            currentSaveGame.Add(key, record);
        }
        record.stringValue = newValue;
    }

    private static void AssertRecordType(string key, Record record, Record.Type recordType)
    {
        if (record.type != recordType)
        {
            throw new SystemException($"SaveGameSystem - AssertRecordType fails reading key {key}, " +
                $"which is {record.type} - tried to read as {recordType}");
        }
    }

    public static string debugJsonContent;
    static void Savegame(Dictionary<string, Record> records)
    {
        List<Record> recordsList = new();
        foreach (KeyValuePair<string, Record> r in records)
        {
            recordsList.Add(r.Value);
        }

        string jsonContent = JsonUtility.ToJson(recordsList);
        debugJsonContent = jsonContent;
    }

    static Dictionary<string, Record> LoadGame(Dictionary<string, Record> records, string jsonContent)
    {
        records.Clear();
        List<Record> recordsList = JsonUtility.FromJson<List<Record>>(jsonContent);
        return recordsList.ToDictionary(x => x.key, x => x);
    }

#if UNITY_EDITOR

    [MenuItem("Save Game System/Save Game")]
    public static void SaveGameDebug() { Savegame(currentSaveGame); }

    [MenuItem("Save Game System/Load Game")]
    public static void LoadGameDebug() { LoadGame(currentSaveGame, debugJsonContent); }

    [MenuItem("Save Game System/Select Save Game 0")]
    public static void SelectSaveGame0() { currentSaveGame = saveGames[0]; }

    [MenuItem("Save Game System/Select Save Game 1")]
    public static void SelectSaveGame1() { currentSaveGame = saveGames[1]; }

    [MenuItem("Save Game System/Select Save Game 2")]
    public static void SelectSaveGame2() { currentSaveGame = saveGames[2]; }

    [MenuItem("Save Game System/Debug/Show Json")]
    public static void DebugShowDebugJson() { Debug.Log(debugJsonContent); }

    [MenuItem("Save Game System/Debug/Set String")]
    public static void DebugSetString() { SetString("123", "456"); }

    [MenuItem("Save Game System/Debug/Get String")]
    public static void DebugGetString() { Debug.Log(GetString("123", "Aquí no hay nah")); }

#endif
}
