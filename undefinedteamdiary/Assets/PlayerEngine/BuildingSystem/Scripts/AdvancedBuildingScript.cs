/*
 * Advanced Building System - nDev Studio
 * Version 1.1
 * */
using UnityEngine;
using System.Collections;

public class AdvancedBuildingScript : MonoBehaviour {
	//Inspector
	public GameObject Player;
	public bool allowCameraMod = true;
	public GameObject CamHolder, CamTargetPos, CamStartPos;
	public Transform spawnPos;
	public Transform[] BuildingObjects;
	public static int CurrentObj = 1;
	public KeyCode enterBuildMod = KeyCode.Tab, changeCamMod = KeyCode.L, changeRotMod = KeyCode.P, changeMoveMod = KeyCode.O;
	public KeyCode forward = KeyCode.UpArrow, backward = KeyCode.DownArrow, left = KeyCode.LeftArrow, right = KeyCode.RightArrow, up = KeyCode.PageUp, down = KeyCode.PageDown, placeObject = KeyCode.KeypadEnter;
	public Vector3 defaultObjPos;
	public Quaternion defaultObjRot;
	public bool playSounds = true;
	public AudioClip placeSound,errorSound;
	public AudioSource audioSource;
	public bool displayObjName = true;
	public GUISkin GuiSkin;
	//
	bool rotateMod = false;
	bool stepMod = false;
	bool cameraMod = false;
	bool buildMod = false;

	void PlaySound(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play ();
	}

	void Start()
	{
		ControlObject (spawnPos, spawnPos, true);
	}
	void Update()
	{
		//Enter/Exit Building Mode
		if (Input.GetKeyDown (enterBuildMod)) {
						if (buildMod == true) {
						     	ControlObject (spawnPos, spawnPos, true);
								buildMod = false;

						} else if (buildMod == false) {
								buildMod = true;
						}
				}

				
		if (buildMod == true) {
						RenderObject (BuildingObjects [CurrentObj]);
						ControlObject (spawnPos,BuildingObjects [CurrentObj],false);
				} else {
			//Reset Cam
			float cameraspeed = 4.0f;
			float increment = 0.0f;
			if (increment <= 1)
				increment += cameraspeed / 100;
			
			CamHolder.transform.position = Vector3.Lerp (CamHolder.transform.position, CamStartPos.transform.position, increment);
			Player.GetComponent<MouseLook> ().enabled = true;
			cameraMod = false;
			ResetObjects();
				}
	}

	void ControlObject(Transform obj, Transform objToSpawn, bool reset)
	{
		if (reset == false) {
						//Move Camera
						float cameraspeed = 4.0f;
						float increment = 0.0f;
			if (cameraMod == true && allowCameraMod == true) {
								if (increment <= 1)
										increment += cameraspeed / 100;
				
								CamHolder.transform.position = Vector3.Lerp (CamHolder.transform.position, CamTargetPos.transform.position, increment);
								Player.GetComponent<MouseLook> ().enabled = false; //HERE CHANGE THE nDEVMOUSELOOK WITH YOUR MOUSELOOK SCRIPT NAME
			} else {
								if (increment <= 1)
										increment += cameraspeed / 100;
				
								CamHolder.transform.position = Vector3.Lerp (CamHolder.transform.position, CamStartPos.transform.position, increment);
								Player.GetComponent<MouseLook> ().enabled = true;
				
						}
						//End Move Camera
						if (Input.GetKeyDown (changeCamMod)) {
								if (cameraMod == true)
										cameraMod = false;
								else
										cameraMod = true;
						}
						if (Input.GetKeyDown (changeRotMod)) {
								if (rotateMod == true)
										rotateMod = false;
								else
										rotateMod = true;
						}
						if (Input.GetKeyDown (changeMoveMod)) {
								if (stepMod == true)
										stepMod = false;
								else
										stepMod = true;
				
						}
						//Block Movement
						if (rotateMod == false) {
								if (stepMod == false) {
										//Smooth Mod
										if (Input.GetKey (forward)) {
												obj.Translate (Vector3.forward * (Time.deltaTime + 0.05f));
										}
										if (Input.GetKey (backward)) {
												obj.Translate (Vector3.back * (Time.deltaTime + 0.05f));
										}
										if (Input.GetKey (left)) {
												obj.Translate (Vector3.left * (Time.deltaTime + 0.05f));
										}
										if (Input.GetKey (right)) {
												obj.Translate (Vector3.right * (Time.deltaTime + 0.05f));
										}		
										if (Input.GetKey (up)) {
												obj.Translate (Vector3.up * (Time.deltaTime + 0.05f));
										}
										if (Input.GetKey (down)) {
												obj.Translate (Vector3.down * (Time.deltaTime + 0.05f));
										}
								} else if (stepMod == true) {
										//Step Mod
										if (Input.GetKeyDown (forward)) {
												obj.Translate (Vector3.forward * BuildingObjects [CurrentObj].localScale.x);
										}
										if (Input.GetKeyDown (backward)) {
												obj.Translate (Vector3.back * BuildingObjects [CurrentObj].localScale.x);
										}
										if (Input.GetKeyDown (left)) {
												obj.Translate (Vector3.left * BuildingObjects [CurrentObj].localScale.z);
										}
										if (Input.GetKeyDown (right)) {
												obj.Translate (Vector3.right * BuildingObjects [CurrentObj].localScale.z);
										}		
										if (Input.GetKeyDown (up)) {
												obj.Translate (Vector3.up * (BuildingObjects [CurrentObj].localScale.y));
										}
										if (Input.GetKeyDown (down)) {
												obj.Translate (Vector3.down * (BuildingObjects [CurrentObj].localScale.y));
										}
								}
				
						}
						//Block Rotation
						if (rotateMod == true) {
								if (Input.GetKey (forward)) {
										obj.Rotate (Vector3.left * (Time.deltaTime + 1));
								}
								if (Input.GetKey (backward)) {
										obj.Rotate (Vector3.right * (Time.deltaTime + 1));
								}
								if (Input.GetKey (left)) {
										obj.Rotate (Vector3.up * (Time.deltaTime + 1));
								}
								if (Input.GetKey (right)) {
										obj.Rotate (Vector3.down * (Time.deltaTime + 1));
								}		
						}
			
						//Place Object
						if (Input.GetKeyDown (placeObject)) {
								if (BuildingObjects [CurrentObj].GetComponent<BuildingObj> ().ObjectAmount > 0) {
				                    	BuildingObjects[CurrentObj].GetComponent<BuildingObj>().enablePreview = false;
										Instantiate (BuildingObjects [CurrentObj].gameObject, obj.position, obj.rotation);
										BuildingObjects [CurrentObj].GetComponent<BuildingObj> ().ObjectAmount -= 1;

					if (playSounds == true)
					{
						PlaySound (placeSound);
					}
								} else {
										Debug.Log ("Not enough Objects");
										CurrentObj = 0;
					if (playSounds == true)
					{
						PlaySound (errorSound);
					}
								}
						}

				} else if (reset == true) {
						obj.localPosition = defaultObjPos;
						obj.localRotation = defaultObjRot;
				}
	}

	void RenderObject (Transform obj)
	{
		if (CurrentObj == 0)
		{
		   cameraMod = false;
		   ResetObjects ();
		} 
		else 
		{
			if (BuildingObjects[CurrentObj].GetComponent<BuildingObj>().ObjectAmount > 0)
			{
				BuildingObjects[CurrentObj].active = true;
				BuildingObjects[CurrentObj].GetComponent<BuildingObj>().enablePreview = true;
			}
			else
			{
				CurrentObj = 0;
				Debug.Log ("Not Enough Objects");
			}
		}
	}

	public void ResetObjects()
	{
		foreach (Transform obj in BuildingObjects) 
		{
			obj.active = false;
		}
	}

	public void AddObjectAmount(int objIndex,int addAmount)
	{
		BuildingObjects [objIndex].GetComponent<BuildingObj> ().ObjectAmount += addAmount;
	}

	void OnGUI()
	{
		GUI.skin = GuiSkin;
		if (displayObjName == true && buildMod == true) 
		{
			string currentObjName = BuildingObjects[CurrentObj].GetComponent<BuildingObj>().ObjectName;
			GUI.Label (new Rect(20,20,250,30),currentObjName);
		}
	}
}
