using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_and_Lobby : MonoBehaviour
{
    public Canvas canvas_Main, canvas_Lobby,canvas_Question,canvas_Selection;
    [SerializeField]
    private int ActiveTeam = 0;

    public Sprite[] all_Skins;
    public Sprite[] all_Icons;

    public string[] TeamName;
    public Sprite[] TeamSkin;
    public Sprite[] TeamIcon;
    public GameObject[] Teams;
    public Quiz_Controller Quiz_ControllerSc;

    public TeamSc[] Team=new TeamSc[6];


    public void Start()
    {
        allClose();
        canvas_Main.gameObject.SetActive(true);
        ExitTeamPanel(null);
        Lobby_To_Start();

        for(int n = 0; n < 6; n++)
        {
            Team[n].set_Name(null);
            Team[n].set_Skin(null);
            Team[n].set_Icon(null);
            Team[n].set_Score(0);
        }
    }
    private void allClose()
    {
        canvas_Lobby.gameObject.SetActive(false);
        canvas_Main.gameObject.SetActive(false);
        canvas_Question.gameObject.SetActive(false);
        canvas_Selection.gameObject.SetActive(false);
    }
    public void Button_Exit()
    {
        Application.Quit();
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
                Teams[n].gameObject.transform.Find("Team_panel_Group/Input_Team_Name").gameObject.GetComponent<TMP_InputField>().text=TeamName[n];
            }
        }


    }
    public void OnTeamPanel(string number)
    {
        //ExitTeamPanel(number); 
        int n = int.Parse(" " + number[5]) - 1;
        ActiveTeam = n+1;
        if (Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Input_Team_Name").gameObject.activeSelf)
        {
            Teams[n].transform.Find("Team_panel_Group/Buttom_DeleteTeam").gameObject.SetActive(true); // включает кнопку "удалить"
            Teams[n].transform.Find("Team_panel_Group/panel_Tone").gameObject.SetActive(true);//включает тонировку
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
                Teams[n].transform.Find("Team_panel_Group/Buttom_DeleteTeam").gameObject.SetActive(false); // выключает кнопку "удалить"
                Teams[n].transform.Find("Team_panel_Group/panel_Tone").gameObject.SetActive(false);// выключает тонировку
            }
        }
    }
    public void Button_AddTeam()
    {
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Team_Skin").gameObject.SetActive(true);// Skin
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Input_Team_Name").gameObject.SetActive(true);// Name
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Button_Add").gameObject.SetActive(false);// Button Add
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
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Team_Skin").gameObject.SetActive(false);// Skin
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Input_Team_Name").gameObject.SetActive(false);// Name
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Button_Add").gameObject.SetActive(true);// Button Add
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/Buttom_DeleteTeam").gameObject.SetActive(false);// Button Delete
        Teams[ActiveTeam - 1].transform.Find("Team_panel_Group/panel_Tone").gameObject.SetActive(false);// тонировка
        TeamName[ActiveTeam - 1] = null;



        int count = 0;
        for (int n = 0; n < TeamName.Length; n++)
        {
            if (TeamName[n] != null)
            {
                count++;
            }
        }
        if(count==0)gameObject.transform.Find("canvas_Lobby/Button_Start").gameObject.GetComponent<Button>().interactable = false;
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
        TeamName[ActiveTeam - 1] = Teams[ActiveTeam - 1].gameObject.transform.Find("Team_panel_Group/Input_Team_Name").gameObject.GetComponent<TMP_InputField>().text;
    }
    public void Button_NextSkin()
    {
        int n = int.Parse(TeamSkin[ActiveTeam - 1].name);
        if (!(n < all_Skins.Length)) n = 0;

        TeamSkin[ActiveTeam - 1] = all_Skins[n];
        //TeamIcon[ActiveTeam - 1] = all_Icons[n];
        VisualizeSkin();
    }
    public void Button_PastSkin()
    {
        int n = int.Parse(TeamSkin[ActiveTeam - 1].name) - 2;
        if (n==-1) n = all_Skins.Length-1;
        TeamSkin[ActiveTeam - 1] = all_Skins[n];
        //TeamIcon[ActiveTeam - 1] = all_Icons[n];
        VisualizeSkin();
    }
    private void VisualizeSkin()
    {
        for (int n = 0; n < 6; n++) 
        {
            Teams[n].gameObject.transform.Find("Team_panel_Group/Team_Skin").gameObject.GetComponent<Image>().sprite = TeamSkin[n];
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
        if (count != 0) 
        {
            int count2 = 0;
            //Debug.Log("game has been started with " + count + " players");

            //string[] TeamName_toStart = new string[count];
            //string[] TeamSkin_toStart = new string[count];
            for (int n = 0; n < TeamName.Length; n++)
            {
                if (TeamName[n] != null)
                {
                    TeamIcon[n] = all_Icons[int.Parse(TeamSkin[n].name)-1];
                    Team[count2].set_Name(TeamName[n]);
                    Team[count2].set_Skin(TeamSkin[n]);
                    Team[count2].set_Icon(TeamIcon[n]);


                    //TeamName_toStart[count2] = TeamName[n];
                    //TeamSkin_toStart[count2] = TeamSkin[n].name;
                    count2++;
                }
            }

            allClose();

            Quiz_ControllerSc.Start_Quiz(Team,count2);
            
        }
        
    }
}
