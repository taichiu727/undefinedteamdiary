using UnityEngine;
using System.Collections;

public class detectEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		photonView = PhotonView.Get(this);
		 

		StartCoroutine(destroy ()); 

	}

	public PhotonView photonView; 

	[RPC]
	IEnumerator destroy() 
	{ 
		yield return new WaitForSeconds (0.1f); 
		PhotonNetwork.Destroy(photonView);
	}
	// Update is called once per frame
	void Update () {
	
	}
	public GameObject delete; 
	 
	[RPC]


	void OnTriggerEnter (Collider other) { 

		{
		if(other.tag == "Player") 
		{ 	
			other.GetComponent<Player_Health>().applyDamage(-100);  


			Debug.Log ("hi");
					

					PhotonNetwork.Destroy(photonView); 

		}
	}
}
}
