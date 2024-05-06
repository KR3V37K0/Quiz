using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team Data", menuName = "Team Data", order = 52)]
public class TeamSc : ScriptableObject
{
    [SerializeField]
    private string Naming;
    [SerializeField]
    private Sprite Skin;
    [SerializeField]
    private Sprite Icon;
    [SerializeField]
    private Sprite Mini;
    [SerializeField]
    private int Score;
    [SerializeField]
    private int Answer;

    public string get_Name   {get{return Naming;}}
    public Sprite get_Skin { get { return Skin; } }
    public Sprite get_Icon { get { return Icon; } }
    public Sprite get_Mini { get { return Mini; } }
    public int get_Score { get { return Score; } }
    public int get_Answer { get { return Answer; } }

    public void set_Name(string naming) { Naming = naming; }
    public void set_Skin(Sprite skin) { Skin=skin; }
    public void set_Icon(Sprite icon) { Icon = icon; }
    public void set_Mini(Sprite mini) { Mini = mini; }
    public void set_Score(int score) { Score = score; }
    public void set_Answer(int a) { Answer = a; }

}