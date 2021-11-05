using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordElement : MonoBehaviour
{
    public Text timeTxt;
    public Text scoreTxt;
    public Text nameTxt;
    
    public void SetInfo(string time, string name, string score)
    {
        timeTxt.text = time;
        scoreTxt.text = score;
        nameTxt.text = name;
    }
}
