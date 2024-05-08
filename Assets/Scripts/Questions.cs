using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System;

public class Questions : MonoBehaviour
{
    public Q_Example[] Qustions_Array;
    public List<Q_Example> Qustions_List = new List<Q_Example>();
    public Quiz_Controller ControllSc;
    //private int count_Stop = 0;
    public Q_Example empty;

    private Q_Example[] randomQ = new Q_Example[4];
    private int[] randomID = new int[4];
    public List<int> IDforShuffle;

    public void Start()
    {
        for(int n=0; n<Qustions_Array.Length; n++)
            Qustions_List.Add(Qustions_Array[n]);
    }
    public void Get4Question()
    {
        //count_Stop = 0;
        randomID = new int[4];
        randomQ = new Q_Example[4];
        //int i = 0;

        IDforShuffle.Clear();
        for (int x = 0; x < Qustions_List.Count; x++)
            IDforShuffle.Add(x);
        shuffle_Cat(IDforShuffle);
        for (int n =0;n<4;n++)
        {
            /*i = Random.Range(0, Qustions_Array.Length-1);*/
            

            randomQ[n] = Qustions_List[IDforShuffle[n]];
            randomID[n] = IDforShuffle[n];

            /*
            for(int z = 0; z < n+1; z++)
            {
                if (randomID[z] == randomID[n]) UniqV(z,randomID);
                else if (randomQ[z] == empty) UniqV(z, randomID);
            }*/
        }
        ControllSc.VizualizeVariants(randomID, randomQ);
    }

    public void shuffle_Cat(List<int> arr)
    {
        Random rand = new Random();

        for (int i = arr.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            int tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }
        IDforShuffle = arr;
    }

    /*public void UniqV(int ID,int[] massive)
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
    }*/
    public void SetCompleted(int ID)
    {
        //Random rand = new Random();
        //Qustions_Array[ID] = Qustions_Array[rand.Next(0, Qustions_Array.Length)];
        Qustions_List.Remove(Qustions_List[ID]);
    }
}
