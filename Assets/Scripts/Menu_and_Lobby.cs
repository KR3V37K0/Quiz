using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_and_Lobby : MonoBehaviour
{
    public Canvas canvas_Main, canvas_Lobby;
    public GameObject panel_Records;
    public Records RecordsSc;
    private int ActiveTeam = 0;

    public Sprite[] all_Skins;

    public string[] TeamName;
    public Sprite[] TeamSkin;
    public GameObject[] Teams;


    public void Start()
    {
        allClose();
        canvas_Main.gameObject.SetActive(true);
        ExitTeamPanel(null);
        Lobby_To_Start();
    }
    private void allClose()
    {
        canvas_Lobby.gameObject.SetActive(false);
        canvas_Main.gameObject.SetActive(false);
        panel_Records.SetActive(false);
    }
    public void Button_Exit()
    {
        Application.Quit();
    }   
    public void Button_Records()
    {
        panel_Records.SetActive(true);
        RecordsSc.LoadRecords();
    }
    public void Button_RecordsClose()
    {
        panel_Records.SetActive(false);
    }
    public void Button_to_MainMenu()
    {
        allClose();
        canvas_Main.gameObject.SetActive(true);
    }
    public void Button_Create_Lobby()
    {
        canvas_Main.gameObject.SetActive(false);
        canvas_Lobby.gameObject.SetActive(true);

        for (int n = 0; n < Teams.Length; n++)
        {
            if(TeamName[n] != null)
            {
                Teams[n].gameObject.transform.GetChild(0).gameObject.transform.Find("Input_Team_Name").gameObject.GetComponent<TMP_InputField>().text=TeamName[n];
            }
        }


    }
    public void OnTeamPanel(string number)
    {
        //ExitTeamPanel(number); 
        int n = int.Parse(" " + number[5]) - 1;
        ActiveTeam = n+1;
        if (Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Input_Team_Name").gameObject.activeSelf)
        {
            Teams[n].transform.GetChild(0).gameObject.transform.Find("Buttom_DeleteTeam").gameObject.SetActive(true); // включает кнопку "удалить"
            Teams[n].transform.GetChild(0).gameObject.transform.Find("panel_Tone").gameObject.SetActive(true);//включает тонировку
        }                                                                                                                                                                                                                                   
    }
    public void ExitTeamPanel(string exclude)
    {

        if (exclude == null) ActiveTeam = 0;          
        else ActiveTeam = int.Parse(" " + exclude[5]);
        for (int n = 0; n < Teams.Length; n++)
        {
            if (Teams[n].name != exclude) 
            { 
                Teams[n].transform.GetChild(0).gameObject.transform.Find("Buttom_DeleteTeam").gameObject.SetActive(false); // выключает кнопку "удалить"
                Teams[n].transform.GetChild(0).gameObject.transform.Find("panel_Tone").gameObject.SetActive(false);// выключает тонировку
            }
        }
    }
    public void Button_AddTeam()
    {
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Team_Skin").gameObject.SetActive(true);// Skin
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Input_Team_Name").gameObject.SetActive(true);// Name
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Button_Add").gameObject.SetActive(false);// Button Add
        TeamName[ActiveTeam - 1] = "Команда "+ActiveTeam;
        Teams[ActiveTeam - 1].gameObject.transform.GetChild(0).gameObject.transform.Find("Input_Team_Name").gameObject.GetComponent<TMP_InputField>().text = TeamName[ActiveTeam - 1];
        OnTeamPanel("Team_"+ActiveTeam);

        int n = ActiveTeam;

        int count = 0;
        for (int x = 0; x < TeamName.Length; x++)
        {
            if (TeamName[x] != null)
            {
                count++;
            }
        }
        if (count != 0) gameObject.transform.Find("canvas_Lobby").gameObject.transform.Find("Button_Start").gameObject.GetComponent<Button>().interactable = true;

        VisualizeSkin();
    }
    public void Button_DeleteTeam()
    {
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Team_Skin").gameObject.SetActive(false);// Skin
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Input_Team_Name").gameObject.SetActive(false);// Name
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Button_Add").gameObject.SetActive(true);// Button Add
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("Buttom_DeleteTeam").gameObject.SetActive(false);// Button Delete
        Teams[ActiveTeam - 1].transform.GetChild(0).gameObject.transform.Find("panel_Tone").gameObject.SetActive(false);// тонировка
        TeamName[ActiveTeam - 1] = null;



        int count = 0;
        for (int n = 0; n < TeamName.Length; n++)
        {
            if (TeamName[n] != null)
            {
                count++;
            }
        }
        if(count==0)gameObject.transform.Find("canvas_Lobby").gameObject.transform.Find("Button_Start").gameObject.GetComponent<Button>().interactable = false;
    }
    public void Lobby_To_Start()
    {
        for(ActiveTeam=6; ActiveTeam > 0; ActiveTeam--)
        {
            Button_DeleteTeam();
        }
        ActiveTeam = 1;
        Button_AddTeam();
        ActiveTeam = 0;
        ExitTeamPanel(null);
        VisualizeSkin();
    }
    public void Set_Name()
    {
        TeamName[ActiveTeam - 1] = Teams[ActiveTeam - 1].gameObject.transform.GetChild(0).gameObject.transform.Find("Input_Team_Name").gameObject.GetComponent<TMP_InputField>().text;
    }
    public void Button_NextSkin()
    {
        int n = int.Parse(TeamSkin[ActiveTeam - 1].name);
        if (!(n < all_Skins.Length)) n = 0;

        TeamSkin[ActiveTeam - 1] = all_Skins[n];
        VisualizeSkin();
    }
    public void Button_PastSkin()
    {
        int n = int.Parse(TeamSkin[ActiveTeam - 1].name) - 2;
        if (n==-1) n = all_Skins.Length-1;
        TeamSkin[ActiveTeam - 1] = all_Skins[n];
        VisualizeSkin();
    }
    private void VisualizeSkin()
    {
        for (int n = 0; n < 6; n++) 
        { 
            Teams[n].gameObject.transform.GetChild(0).gameObject.transform.Find("Team_Skin").gameObject.GetComponent<Image>().sprite = TeamSkin[n];
        }
    }



    public void Button_Start_Game()
    {
        int count = 0;
        for(int n=0; n < TeamName.Length; n++)
        {
            if (TeamName[n] != null)
            {
                count++;
            }
        }
        if (count != 0) Debug.Log("game has been started with " + count + " players");
        
    }
}
