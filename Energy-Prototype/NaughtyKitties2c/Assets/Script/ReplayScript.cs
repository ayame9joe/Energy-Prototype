using UnityEngine;
using System.Collections;

public class ReplayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Replay ()
	{
		GameManager.hp = 100;
		GameManager.dps = 0;
		GameManager.enemy = 20;
		GameManager.totalEnergy = 5;
		Application.LoadLevel (Application.loadedLevel);
		this.gameObject.SetActive (false);
	}
}
