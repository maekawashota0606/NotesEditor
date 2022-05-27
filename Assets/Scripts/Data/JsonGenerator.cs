using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonGenerator : SingletonMonoBehaviour<JsonGenerator>
{
    private const string _FOLDER_NAME = "JSON";

    public void Generate(SheetData data)
    {
        // jsonファイル名
        string fileName = $"{data.musicID}_{data.course}.json";


        if(!Directory.Exists(Application.dataPath + "/" + _FOLDER_NAME))
        {
            Debug.Log("create file");
            Directory.CreateDirectory(Application.dataPath + "/" + _FOLDER_NAME);
        }

        // 出力パスを指定、書き出し形式は上書き
        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + _FOLDER_NAME + "/" + fileName, false);
        
        
        //Debug.Log(Application.dataPath + "/" + _FOLDER_NAME + "/" + fileName);
        string jsonData = JsonUtility.ToJson(data);
        //Debug.Log(jsonData);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();

        AudioManager.Instance.PlayCompleteSE();
    }
}