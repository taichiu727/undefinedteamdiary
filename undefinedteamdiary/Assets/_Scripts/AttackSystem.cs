using UnityEngine;
using System.Collections;

public class AttackSystem : MonoBehaviour {

	public Transform spawnPos; 
	public GameObject detect; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.G)) 
		{ 
			PhotonNetwork.Instantiate("collisionDetect", spawnPos.position, spawnPos.rotation, 0); 
		}
	}
}
