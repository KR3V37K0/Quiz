using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Question", menuName = "Question Data", order = 51)]
public class Q_Example : ScriptableObject
{
    [SerializeField]
    private string Category;
    [SerializeField]
    private string Text;
    [SerializeField]
    private Sprite Photo;
    [SerializeField]
    private GameObject Video;
    [SerializeField]
    private string True;
    [SerializeField]
    private string[] False;
    [SerializeField]
    private int Score;

    public string category
    {get
        {
            return Category;
        }
    }
    public string text
    {
        get
        {
            return Text;
        }
    }
    public Sprite photo
    {
        get
        {
            return Photo;
        }
    }
    public GameObject video
    {
        get
        {
            return Video;
        }
    }
    public string answer
    {
        get
        {
            return True;
        }
    }
    public string[] lie
    {
        get
        {
            return False;
        }
    }
    public int score
    {
        get
        {
            return Score;
        }
    }
}
