using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    public Q_Example[] Qustions_Array;


    public void Start()
    {
        Debug.Log(Qustions_Array[0].category);
    }

}
