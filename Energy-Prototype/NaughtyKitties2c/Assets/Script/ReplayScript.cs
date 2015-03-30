using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReplayScript : MonoBehaviour {

	public Text turns;
	public Text isWinning;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.enemy < 0) {
			isWinning.text = "You Win in ";
		} else if (GameManager.hp < 0) {
			isWinning.text = "You Lose in ";
		}

		turns.text = GameManager.turn + " " + "Turns";
	
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
