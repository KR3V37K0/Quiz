using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Team_panel_Touch : MonoBehaviour
{
    public Menu_and_Lobby LobbySc;
    void Start()
    {
        LobbySc = GameObject.Find("Main Camera").GetComponent<Menu_and_Lobby>();
        Debug.Log(LobbySc.gameObject.name);
    }
    void Update()
    {
        
    }
    public void OnMouseEnter()
    {
        LobbySc.OnTeamPanel(gameObject.transform.parent.gameObject.transform.parent.gameObject.name);
    }
    public void OnMouseExit()
    {
        LobbySc.ExitTeamPanel(null);
    }
}
