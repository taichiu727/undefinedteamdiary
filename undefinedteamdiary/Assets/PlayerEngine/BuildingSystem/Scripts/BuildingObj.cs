/*
 * Advanced Building System - nDev Studio
 * Version 1.0 Alpha
 * */
using UnityEngine;
using System.Collections;

public class BuildingObj : MonoBehaviour {
	public string ObjectName = "Building Material";
	public int ObjectAmount = 10;
	public Color normalColor = Color.white, previewColor = Color.green;
	public bool enablePreview = false;

	void Update()
	{
		if (enablePreview == true && gameObject.renderer.material.color != previewColor) {
						gameObject.renderer.material.color = previewColor;
		} else if (enablePreview == false && gameObject.renderer.material.color != normalColor){
						gameObject.renderer.material.color = normalColor;
				}
	}
}
