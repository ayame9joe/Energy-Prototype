using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour {

	public Toggle autoTurnEndTog;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (autoTurnEndTog.isOn) {
			GameManager.autoEndTurn = true;
		} else {
			GameManager.autoEndTurn = false;
		}
	
	}




}
