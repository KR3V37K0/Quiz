using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_and_Lobby : MonoBehaviour
{
    public Canvas canvas_Main, canvas_Lobby;

    public Sprite[] all_Skins;

    public GameObject[] Teams;
    public void Start()
    {
        allClose();
        canvas_Main.gameObject.SetActive(true);
        ExitTeamPanel(null);
    }
    private void allClose()
    {
        canvas_Lobby.gameObject.SetActive(false);
        canvas_Main.gameObject.SetActive(false);
    }
    public void Button_Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }   
    public void Button_to_MainMenu()
    {
        allClose();
        canvas_Main.gameObject.SetActive(true);
    }
    public void Button_Start_Game()
    {
        Debug.Log("game has been started");
    }
    public void Button_Create_Lobby()
    {
        canvas_Main.gameObject.SetActive(false);
        canvas_Lobby.gameObject.SetActive(true);
        
    }
    public void OnTeamPanel(string number)
    {
        ExitTeamPanel(number);
        Debug.Log(Teams[int.Parse(" " + number[5]) - 1].name + "is close");
        //Teams[int.Parse(" "+number[5])-1].transform.gameObject.transform.Find("Button_DeleteTeam").gameObject.SetActive(false);
    }
    public void ExitTeamPanel(string exclude)
    {
        if (exclude == null) 
        { 
            //Debug.Log("all closed"); 
            for(int n=0; n < Teams.Length; n++)
            {
                Debug.Log(Teams[n].name + "is close");
                //Teams[n].transform.gameObject.transform.Find("Buttom_DeleteTeam").gameObject.SetActive(false);
            }
        }
        else Debug.Log(exclude + " is active");
    }

}
