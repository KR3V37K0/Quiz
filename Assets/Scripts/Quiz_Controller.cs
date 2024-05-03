using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;
using System;



public class Quiz_Controller : MonoBehaviour
{
    public Canvas canvas_Question, canvas_Selection;
    public Menu_and_Lobby LobbySc;
    public Questions QuestionsSc;
    private GameObject Buttons;
    private Button Ver1, Ver2, Ver3, Ver4;

    public Q_Example ThisQuestion;
    private Q_Example[] Var4Question=new Q_Example[4];
    private int[] Var4ID=new int[4];

    public TMP_Text Q_score, Q_Category, Q_Text;
    public TMP_Text[] Answers_text;
    public Button button_Image_View;

    private string[] Lies;


    void Start()
    {
        Buttons = gameObject.transform.Find("canvas_Selection/Buttons").gameObject;
        
    }
    void Update()
    {

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
    public void Start_Quiz(string[] name,string[] skin)
    {
        Debug.Log("STARTED");
        QuestionsSc.Get4Question();
        canvas_Selection.gameObject.SetActive(true);
        //foreach (string n in name) {  Debug.Log(n);}
       
    }
    public void VizualizeVariants(int[] ID, Q_Example[] Questions)
    {
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

        Debug.Log(Var4ID[var_Button]);
        QuestionsSc.SetCompleted(Var4ID[var_Button]);

        Question_Viev();
    }


    public void Question_Viev()
    {
        canvas_Selection.gameObject.SetActive(false);
        canvas_Question.gameObject.SetActive(true);
        canvas_Question.gameObject.transform.Find("style_classic").gameObject.SetActive(true);
        Debug.Log(ThisQuestion.photo);
        if (ThisQuestion.photo!=null)
        {
            button_Image_View.gameObject.SetActive(true);
        }
        else button_Image_View.gameObject.SetActive(false);
        Random rand = new Random();
        Answers_text[rand.Next(0, 4)].text = ThisQuestion.answer;
        int c = 0;
        shuffle_Lie(ThisQuestion.lie);
        for (int count=0; count < 4; count++)
        {          
            if (Answers_text[count].text == "ÎÒÂÅÒ îòâåò ")
            {
                Answers_text[count].text = Lies[c];
                c++;
            }
        }
        Q_score.text = ThisQuestion.score.ToString();
        Q_Category.text = ThisQuestion.category;
        Q_Text.text = ThisQuestion.text;
    }
    public void Button_SetAnswer(int button_numb)
    {

    }
    public void Button_SetPlayer(int playerID)
    {

    }
}
