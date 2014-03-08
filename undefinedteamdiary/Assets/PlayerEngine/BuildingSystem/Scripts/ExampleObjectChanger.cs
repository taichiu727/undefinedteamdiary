/*
 * Advanced Building System - nDev Studio
 * Version 1.0 Alpha
 * */
using UnityEngine;
using System.Collections;

public class ExampleObjectChanger : MonoBehaviour {
	public GameObject buildingSystemHolder;
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 1;
				}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 2;
				}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 3;
				}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 4;
				}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 5;
				}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 6;
				}
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 7;
				}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 8;
				}
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 9;
				}
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
						buildingSystemHolder.GetComponent<AdvancedBuildingScript> ().ResetObjects ();
						AdvancedBuildingScript.CurrentObj = 0; //Exit Building Mod
				}
	}
}
