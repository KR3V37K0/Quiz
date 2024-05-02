using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    public Q_Example[] Qustions_Array;
    public Quiz_Controller ControllSc;
    private int count_Stop = 0;
    public Q_Example empty;

    private Q_Example[] randomQ = new Q_Example[4];
    private int[] randomID = new int[4];

    public void Start()
    {
    }
    public void Get4Question()
    {
        count_Stop = 0;
        randomID = new int[4];
        randomQ = new Q_Example[4];
        int i = 0;


        for(int n =0;n<4;n++)
        {
            i = Random.Range(0, Qustions_Array.Length-1);
            randomQ[n] = Qustions_Array[i];
            randomID[n] = i;

            
            for(int z = 0; z < n+1; z++)
            {
                if (randomID[z] == randomID[n]) UniqV(z,randomID);
                else if (randomQ[z] == empty) UniqV(z, randomID);
            }
        }
        ControllSc.VizualizeVariants(randomID, randomQ);
    }
    public void UniqV(int ID,int[] massive)
    {
        count_Stop++;
        if (count_Stop > Qustions_Array.Length) { Debug.Log("ERROR"+count_Stop);return; }
        for (int z = 0; z < 4; z++)
        {
            
            if (massive[ID] != 999) 
            {
                if (((massive[z] == massive[ID])||(Qustions_Array[massive[ID]]==empty))&&(z!=ID)) massive[ID] = 999;
            }
        }
        if (massive[ID] == 999)
        {           
            massive[ID] = count_Stop;
            UniqV(ID, massive);
        }
        else
        {
            randomID = massive;
            randomQ[ID] = Qustions_Array[randomID[ID]];
            Debug.Log("EXELENT!!!");
            return;
        }
    }
    public void SetCompleted(int ID)
    {
        Qustions_Array[ID] = empty;
    }
}
