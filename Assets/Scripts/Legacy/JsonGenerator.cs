using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonGenerator : SingletonMonoBehaviour<JsonGenerator>
{
    public void Generate(SheetData data)
    {
        // �o�̓p�X
        string filePath = "file://" + Application.dataPath + "/StreamingAssets/";

        // json�t�@�C����
        string fileName = $"{data.musicID}_{data.course}.json";

        // �o��
        StreamWriter writer = new StreamWriter(filePath + fileName, true);
        string jsonData = JsonUtility.ToJson(data);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }
}