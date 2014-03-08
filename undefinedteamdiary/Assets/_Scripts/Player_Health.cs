using UnityEngine;
using System.Collections;

public class Player_Health : Photon.MonoBehaviour
{
    public float curHP = 3000f;
    public float maxHP = 3000f;
    public float maxBAR = 3000f;
    public float HealthBarLength;
	public float curMP = 2000f; 
	public float maxMP = 2000f; 
	public float MPBarLength; 
	
	public PhotonView photonView; 

	Transform respawnPos; 

	void Start() 
	{ 
		photonView = PhotonView.Get (this); 
	}
    void OnGUI()
    {	

		
			if(Input.GetKeyDown(KeyCode.Q)) 
			{ 
				curHP -= 10f; 
				if(curHP <0) 
				{ 
					curHP = 0; 
				}
			}
			GUI.backgroundColor = Color.red;
        	GUI.Box(new Rect(100, 60, HealthBarLength, 25), "");      //This will create the health bar at the coordinates 10,10 
			GUI.Box (new Rect (100, 40, 200, 20), curHP + " / " + maxHP);
			GUI.Box(new Rect(100, 105, MPBarLength, 25), "");      //This will create the health bar at the coordinates 10,10 
			GUI.Box (new Rect (100, 85, 200, 20), curMP + " / " + maxMP);
		 // change back ground to red 
        	HealthBarLength = curHP * maxBAR / maxHP;               // Determines the length
			MPBarLength = curMP * maxBAR / maxMP; 
		}
    
	void Update () 
	{ 

			if(curHP == 0) 
			{ 
				//photonView.RPC ("respawn" , PhotonTargets.All); 
				curHP = maxHP;
			}
			
	}
	[RPC]
	public void ChangeHP(float Change, PhotonMessageInfo info)
    {
        curHP = curHP += Change;
       

        if (curHP == 0)
        {
            Debug.Log("Player Has Died!");
        }

		if(curHP <0) 
		{ 
			curHP = 0; 
		}

    } 

	bool dead = false; 

	[RPC] 
	public void respawn(PhotonMessageInfo info) 
	{ 
		PhotonNetwork.Destroy (photonView	); 
		PhotonNetwork.Instantiate ("Charprefab", respawnPos.position, respawnPos.rotation, 0); 
	}

	public void applyDamage(float dmg) 
	{ 
		photonView.RPC("ChangeHP",PhotonTargets.All,dmg); 
	}

}
