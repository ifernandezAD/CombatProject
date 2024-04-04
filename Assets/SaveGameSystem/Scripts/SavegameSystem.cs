using System;
using System.Collections.Generic;
using UnityEngine;

public class SavegameSystem : MonoBehaviour
{
    class Record
    {
        public enum Type
        {
            String,
            Int,
            Float,
            Bool,
        }

        internal Type type;
        internal string stringValue;
        internal int intValue;
        internal float floatValue;
        internal bool boolValue;
    };


    Dictionary<string, Record> currentSaveGame;

    Dictionary<string, Record>[] saveGames;

    public string GetString(string key, string defaultValue = "")
    {
        string retVal = defaultValue;
        if (currentSaveGame.TryGetValue(key, out Record record))
        {
            AssertRecordType(key,record, Record.Type.String);
            retVal = record.stringValue;
        }
        return retVal;
    }

    public void SetString(string key, string newValue)
    {
        Record record;
        if (!currentSaveGame.TryGetValue(key, out record))
        {
            record = new();
            record.type = Record.Type.String;
        }
        record.stringValue = newValue;
    }

    private void AssertRecordType(string key, Record record, Record.Type recordType)
    {
        if (record.type != recordType)
        {
            throw new SystemException($"SaveGameSystem - AssertRecordType fails reading key {key}, " +
                $"which is {record.type} - tried to read as {recordType}");
        }
    }
}
