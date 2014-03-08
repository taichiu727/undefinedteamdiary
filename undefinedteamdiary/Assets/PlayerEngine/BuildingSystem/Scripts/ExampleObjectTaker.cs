using UnityEngine;
using System.Collections;

public class ExampleObjectTaker : MonoBehaviour {
	public int addNumber = 5;
	public GameObject buildingSystemHolder;
	void Update () {
	if (Input.GetKeyDown (KeyCode.F1))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (1,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F2))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (2,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F3))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (3,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F4))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (4,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F5))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (5,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F6))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (6,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F7))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (7,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F8))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (8,addNumber);
		}
		if (Input.GetKeyDown (KeyCode.F9))
		{
			buildingSystemHolder.GetComponent<AdvancedBuildingScript>().AddObjectAmount (9,addNumber);
		}
	}
}
