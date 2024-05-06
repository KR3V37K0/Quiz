using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;
using System;
using Unity.Burst.Intrinsics;



public class Quiz_Controller : MonoBehaviour
{
    public int Max_Rounds;
    private int round = 0;

    private TeamSc[] Teams = new TeamSc[6];
    private int PlayerCount = 0;
    public GameObject[] TeamsAvatar;

    public Canvas canvas_Question, canvas_Selection,canvas_Teams,canvas_Win;
    public Menu_and_Lobby LobbySc;
    public Questions QuestionsSc;

    private GameObject Buttons;
    public Button[] ButtonVer = new Button[4];

    public Q_Example ThisQuestion;
    private Q_Example[] Var4Question=new Q_Example[4];
    private int[] Var4ID=new int[4];

    public TMP_Text Q_score, Q_Category, Q_Text;
    public TMP_Text[] Answers_text;
    public Button button_Image_View;
    public Button button_OK;
    private GameObject panel_Image;
    public int SelectedTeam;

    private string[] Lies;
    public int True = 4;
    private int[] b = new int[4];


    void all_Close()
    {
        canvas_Question.gameObject.SetActive(false);
        canvas_Selection.gameObject.SetActive(false);
        canvas_Teams.gameObject.SetActive(false);
        canvas_Win.gameObject.SetActive(false);
    }
    void Start()
    {
        for(int n = 0; n < 4; n++)
        {
            ButtonVer[n] = canvas_Question.gameObject.transform.Find("style_classic/Buttons/Button_Answer " + (n+1)).gameObject.GetComponent<Button>();
        }
        canvas_Teams.gameObject.SetActive(false);
        Buttons = gameObject.transform.Find("canvas_Selection/Buttons").gameObject;
        panel_Image = gameObject.transform.Find("canvas_Question/Panel_Image").gameObject;
        for(int n = 0; n < 6; n++)
        {
            TeamsAvatar[n].SetActive(false);
        }
    }
    public void shuffle_Lie(string[] arr)
    {
        Random rand = new Random();

        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            string tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }
        Lies = arr;
    }
    public void Start_Quiz(TeamSc[] teams,int count)
    {
        round = 0;
        Teams = teams;
        PlayerCount = count;
        for (int n = 0; n < 6; n++)
        {
            if (Teams[n].get_Name == null) Teams[n] = null;
        }
        Create_Teams();
        QuestionsSc.Get4Question();
        canvas_Selection.gameObject.SetActive(true);
       
    }
    public void VizualizeVariants(int[] ID, Q_Example[] Questions)
    {
        panel_Image.SetActive(false);
        canvas_Question.gameObject.SetActive(false);
        canvas_Selection.gameObject.SetActive(true );
        


        Var4ID = ID;
        Var4Question = Questions;
        for(int n = 0; n < 4; n++)
        {
            canvas_Selection.gameObject.transform.Find("Buttons/Button_Variant " + (n + 1) + "/Button/Text (TMP)").gameObject.GetComponent<TMP_Text>().text = "   " + Var4Question[n].category;
            canvas_Selection.gameObject.transform.Find("Buttons/Button_Variant " + (n + 1) + "/Button/Score").gameObject.GetComponent<TMP_Text>().text = "   " + Var4Question[n].score;
        }



    }
    public void Button_Variant(int var_Button)
    {
        ThisQuestion = Var4Question[var_Button];
        QuestionsSc.SetCompleted(Var4ID[var_Button]);

        Question_Viev();
    }


    public void Question_Viev()
    {
        round++;
        button_OK.gameObject.SetActive(false);
        if (ThisQuestion.photo) panel_Image.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ThisQuestion.photo;

        canvas_Selection.gameObject.SetActive(false);
        canvas_Question.gameObject.SetActive(true);
        canvas_Question.gameObject.transform.Find("style_classic").gameObject.SetActive(true);

        if (ThisQuestion.photo!=null)
        {
            button_Image_View.gameObject.SetActive(true);
        }
        else button_Image_View.gameObject.SetActive(false);
        Random rand = new Random();
        True = rand.Next(0, 4);
        Answers_text[True].text = ThisQuestion.answer;
        int c = 0;
        shuffle_Lie(ThisQuestion.lie);
        for (int count=0; count < 4; count++)
        {          
            if (Answers_text[count].text != ThisQuestion.answer)
            {
                Answers_text[count].text = Lies[c];
                c++;
            }
        }
        Q_score.text = ThisQuestion.score.ToString();
        Q_Category.text = ThisQuestion.category;
        Q_Text.text = ThisQuestion.text;
    }
    public void Button_Image()
    {
        if (panel_Image.activeSelf) panel_Image.SetActive(false);
        else panel_Image.SetActive(true);
    }
    public void Create_Teams()
    {
        int sw = Screen.width;
        canvas_Teams.gameObject.SetActive(true);
        for(int n = 0; n < PlayerCount; n++)
        {
            Teams[n].set_Answer(4);
            TeamsAvatar[n].transform.Find("ICO/Text_Name").gameObject.GetComponent<TMP_Text>().text = Teams[n].get_Name;
            TeamsAvatar[n].transform.Find("ICO/Text_Score").gameObject.GetComponent<TMP_Text>().text = "0";
            TeamsAvatar[n].transform.Find("ICO/Image").gameObject.GetComponent<Image>().sprite = Teams[n].get_Icon;
            TeamsAvatar[n].transform.localPosition = new Vector3((sw/-2)+((n+0.5f)*sw/PlayerCount), TeamsAvatar[n].transform.localPosition.y, 0);
            TeamsAvatar[n].SetActive(true);
        }
    }
    public void Button_SetAnswer(int button_numb)
    {
        if (Teams[SelectedTeam].get_Answer !=button_numb) 
        { 
            if (Teams[SelectedTeam].get_Answer != 4)
            {
                ButtonVer[Teams[SelectedTeam].get_Answer].gameObject.transform.Find("char " + b[Teams[SelectedTeam].get_Answer]).gameObject.SetActive(false);
                b[Teams[SelectedTeam].get_Answer] -=1;
            }
            b[button_numb]++;
            ButtonVer[button_numb].gameObject.transform.Find("char " + b[button_numb]).gameObject.GetComponent<Image>().sprite = Teams[SelectedTeam].get_Mini;
            ButtonVer[button_numb].gameObject.transform.Find("char " + b[button_numb]).gameObject.SetActive(true);
            Teams[SelectedTeam].set_Answer(button_numb);
        }
        for(int n = 0; n < PlayerCount; n++)
        {
            button_OK.gameObject.SetActive(true);
            if(Teams[n].get_Answer==4) button_OK.gameObject.SetActive(false);
        }

    }
    public void Button_SetPlayer(int playerID)
    {
        SelectedTeam = playerID;
    }
    public void Button_OK_Answers()
    {
        StartCoroutine(Calculate());
    }
    public IEnumerator Calculate()
    {
        button_OK.gameObject.SetActive(false);
        ButtonVer[True].interactable = false;
        for (int n = 0; n < PlayerCount; n++)
        {
            if (Teams[n].get_Answer == True)
            {
                TeamsAvatar[n].gameObject.transform.Find("ICO/Text_Score").gameObject.GetComponent<TMP_Text>().color = Color.green;
                Teams[n].set_Score(Teams[n].get_Score + ThisQuestion.score);                
            }
            else
            {
                TeamsAvatar[n].gameObject.transform.Find("ICO/Text_Score").gameObject.GetComponent<TMP_Text>().color = Color.red;
                Teams[n].set_Score(Teams[n].get_Score - ThisQuestion.score);
            }
            Teams[n].set_Answer(4);
            TeamsAvatar[n].gameObject.transform.Find("ICO/Text_Score").gameObject.GetComponent<TMP_Text>().text = Teams[n].get_Score.ToString();
            yield return new WaitForSeconds(1.5f);
        }
        for (int n = 0; n < PlayerCount; n++)
            TeamsAvatar[n].gameObject.transform.Find("ICO/Text_Score").gameObject.GetComponent<TMP_Text>().color = Color.white;


        ButtonVer[True].interactable = true;
        for(int n = 0; n < 4; n++)
        {
            for(int n2 = 1;n2 < 7; n2++) 
            {
                ButtonVer[n].gameObject.transform.Find("char " + n2).gameObject.SetActive(false);
            }
            b[n] = 0;
        }
        int maxScore=0, countWiner = 0;
        for (int n = 0;n < PlayerCount;n++)
        {
            if (Teams[n].get_Score>maxScore)maxScore = Teams[n].get_Score;
        }
        for (int n = 0; n < PlayerCount; n++)
        {
            if (Teams[n].get_Score == maxScore) countWiner++;       
        }     
        if ((round>=Max_Rounds)&&(countWiner==1)) StartCoroutine(Win());
        else QuestionsSc.Get4Question();

    }
    public IEnumerator Win()
    {
        all_Close();
        canvas_Win.gameObject.transform.Find("Panel/Text_Winner").gameObject.SetActive(false);
        canvas_Win.gameObject.transform.Find("Image_WinnerSkin").gameObject.SetActive(false);
        canvas_Win.gameObject.SetActive(true);

        TeamSc[] Rating = Teams;
        TeamSc temp;

        for (int i = 0; i < PlayerCount; i++) 
        { for (int j = i + 1; j < PlayerCount; j++) 
            { if (Rating[i].get_Score < Rating[j].get_Score)
                {
                    temp = Rating[i];
                    Rating[i] = Rating[j];
                    Rating[j] = temp;
                }
            }
        }
        for (int i = PlayerCount-1; i > -1; i--) 
        {
            yield return new WaitForSeconds(1.5f);
            //Debug.Log(Rating[i].get_Score.ToString());
            canvas_Win.gameObject.transform.Find("Panel/Place " + (i+1)).gameObject.SetActive(true);
            canvas_Win.gameObject.transform.Find("Panel/Place " + (i + 1) + "/Panel_Score/Text_Name").gameObject.GetComponent<TMP_Text>().text = Rating[i].get_Name;
            canvas_Win.gameObject.transform.Find("Panel/Place " + (i + 1) + "/Panel_Score/Text_Score").gameObject.GetComponent<TMP_Text>().text = Rating[i].get_Score.ToString();
            canvas_Win.gameObject.transform.Find("Panel/Place " + (i + 1) + "/Panel_Score/Image_Mini").gameObject.GetComponent<Image>().sprite = Rating[i].get_Mini;
            canvas_Win.gameObject.transform.Find("Panel/Place " + (i + 1) + "/Panel_Score/Image_Mini/Text_Place").gameObject.GetComponent<TMP_Text>().text = (i + 1).ToString();


        }
        canvas_Win.gameObject.transform.Find("Panel/Text_Winner").gameObject.SetActive(true);
        canvas_Win.gameObject.transform.Find("Image_WinnerSkin").gameObject.SetActive(true);
        canvas_Win.gameObject.transform.Find("Image_WinnerSkin").gameObject.GetComponent<Image>().sprite = Rating[0].get_Skin;

        yield return new WaitForSeconds(1.5f);
    }
}
