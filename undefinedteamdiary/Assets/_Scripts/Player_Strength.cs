using UnityEngine;
using System.Collections;

public class Player_Strength : MonoBehaviour
{
    public float curSTR = 100f;
    public float maxSTR = 100f;
    public float maxBAR = 100f;
    public float StrengthBarLength;

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, StrengthBarLength, 25), "");      //This will create the health bar at the coordinates 10,10
        StrengthBarLength = curSTR * maxBAR / maxSTR;               // Determines the length 
    }

    void ChangeSTR(float Change)
    {
        curSTR += Change;
        if (curSTR > 100)
        {
            curSTR = 100;
        }

        if (curSTR < 0)
        {
            Debug.Log("Player Has Ran Out Of Strength");
        }

    }
}
