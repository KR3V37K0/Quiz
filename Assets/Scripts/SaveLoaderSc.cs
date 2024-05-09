using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoaderSc : MonoBehaviour
{
    private void Start()
    {
        //SaveRecord("team1", 500, "10.20.2013");
        //SaveRecord("team2", 1000, "13.20.2013");
        //SaveRecord("team3", 1500, "15.20.2013");
        //SaveRecord("team4", 1500, "15.20.2013");
        //LoadRecord();
    }
    public void SaveRecord(string Name, int Score, string Date)
    {
        int files_Count = 0;
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/Records"); //Application.streamingAssetsPath для винды????
        foreach(string file in files)
        {
            if (file[file.Length-1]!='a')files_Count++;
        }
        Debug.Log(files_Count);
        Record new_Record = new Record();
        new_Record.Name = Name;
        new_Record.Score = Score;
        new_Record.Data = Date;
        string json_setting_old = null;


        string json_settings = JsonUtility.ToJson(new_Record);
        File.WriteAllText(Application.streamingAssetsPath + "/Records/"+files_Count+"records.json", json_settings);
    }
    public List<Record> LoadRecord()
    {
        int files_Count = 0;
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/Records"); //Application.streamingAssetsPath для винды????
        foreach (string file in files)
        {
            if (file[file.Length - 1] != 'a') files_Count++;
        }
        List<Record> all_records=new List<Record>();
        for(int n=0; n<files_Count; n++)
        {
            string json_setting = File.ReadAllText(Application.streamingAssetsPath + "/Records/"+n+"records.json");
            Record old_data_settings = JsonUtility.FromJson<Record>(json_setting);
            all_records.Add(old_data_settings);
        }


        return(all_records);
    }
}

[Serializable]
public class Record
{
    public string Name;
    public int Score;
    public string Data;
}
