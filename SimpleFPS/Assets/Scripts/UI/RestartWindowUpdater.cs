using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartWindowUpdater : MonoBehaviour {

    public Text accuracyTxt;
    public Text topAccuracyTxt;
	
    public void UpdateText(float accuracy, float topAccuracy)
    {
        accuracyTxt.text = accuracy + " %";
        topAccuracyTxt.text = topAccuracy + " %";
    }
}
