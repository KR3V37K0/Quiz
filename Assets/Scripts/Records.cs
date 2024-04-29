using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Records : MonoBehaviour
{
    [System.Serializable]
    public class Record
    {
        public int Place;
        public int Score;
        public string Team;
        public string Game;

        public Record()
        {
            Place = new int();
            Score = new int();

        }

    }
    public void LoadRecords()
    {
        Debug.Log("Records has Loaded");
    }
}
