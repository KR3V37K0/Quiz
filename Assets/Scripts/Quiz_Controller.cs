using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void Start()
    {
        Buttons = gameObject.transform.Find("canvas_Selection/Buttons").gameObject;
    }
    void Update()
    {
        
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
        if((ThisQuestion.photo==null)&&(ThisQuestion.video == null))
        {
            canvas_Question.gameObject.transform.Find("style_classic").gameObject.SetActive(true);
            Debug.Log("activated");
        }
    }
}
