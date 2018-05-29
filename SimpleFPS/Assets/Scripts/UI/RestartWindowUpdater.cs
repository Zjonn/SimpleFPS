using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartWindowUpdater : MonoBehaviour {

    public Text accuracy;
    public Text topAccuracy;
	
    public void UpdateText(float accuracy, float topAccuracy)
    {
        this.accuracy.text = accuracy + "% trafień";
        this.topAccuracy.text = topAccuracy + "% trafień";
    }
}
