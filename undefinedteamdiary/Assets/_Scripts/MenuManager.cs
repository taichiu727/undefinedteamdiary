using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
    public string CurrentMenu;

	void Start () 
    {
        CurrentMenu = "Main";
	}
	
	void Update () {
	
	}

    void OnGUI()
    {
        if (CurrentMenu == "Main")
            Menu_Main();
    }

    private void Menu_Main()
    {
        if (GUI.Button(new Rect(10, 10, 128, 30), "Quit"))
        {
            Application.Quit();
        }
    }

}
