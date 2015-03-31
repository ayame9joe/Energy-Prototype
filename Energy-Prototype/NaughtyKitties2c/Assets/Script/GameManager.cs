using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// --- Vals ---
	public static float hp = 100;
	public static float dps;
	public static float enemy = 20;
	public static float totalEnergy = 5;

	public static float attackVal;
	public static float energyVal;
	public static float repairVal;
	public static float enpowerVal;

	public static bool isTurnEnd;
	public static float turn;

	public static bool isCatTurnEnd;
	public static float catTurn;

	public static float autoEnergyTurn;

	//public static float autoCatTurn;

	// --- UI ---

	public Text txtTotalEnergy;
	public Text txtDps;
	public Text txtHp;
	public Text txtEnemy;

	public Text txtTurn;
	public Text txtCatTurn;

	public Text txtAttackVal;
	public Text txtEnergyVal;
	public Text txtEnpowerVal;
	public Text txtRepairVal;

	public Text txtAutoTurn;

	public Toggle autoTurnEndTog;

	//--- Boolen ---
	public static bool isAttack;
	public static bool isEnergy;
	public static bool isRepair;
	public static bool isEnpower;


	public static bool isCatMode;
	public static bool autoEndTurn;

	//--- Panels ---
	public GameObject ModePanel;
	public GameObject GamePanel;
	public GameObject ReplayPanel;

	// Use this for initialization
	void Start () {

		GamePanel.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {

		ShowUI ();

		if (autoEndTurn && GameObject.Find("Gos")) {
			//isTurnEnd = true;
			//isCatTurnEnd = true;
			if (GameObject.Find ("BtnEndEnergyTurn"))
			{
				GameObject.Find ("BtnEndEnergyTurn").SetActive (false);
			}
			if (GameObject.Find ("BtnEndCatTurn"))
			{
				GameObject.Find ("BtnEndCatTurn").SetActive (false);
			}
		} else if (!autoEndTurn && GameObject.Find("Gos")){
			if (isTurnEnd) {
				
				CheckVal();
				RandomlyHarming();
				Attack();
				isTurnEnd = false;
			}
			
			if (isCatTurnEnd) {
				
				CheckVal();
				RandomlyHarming();
				Attack();
				isCatTurnEnd = false;
			}


		}


	
		if (enemy < 0 || hp < 0) {

			ReplayPanel.SetActive(true);
		}


		if (!isCatMode) {
			GameObject[] outGos = GameObject.FindGameObjectsWithTag ("OnlyInCat");
			for (int i = 0; i < outGos.Length; i++) {
				outGos [i].SetActive (false);
			}
			GameObject[] inGos = GameObject.FindGameObjectsWithTag ("OnlyInEnergy");
			for (int i = 0; i < inGos.Length; i++) {
				inGos [i].SetActive (true);
			}		
		} else {
			GameObject[] outGos = GameObject.FindGameObjectsWithTag("OnlyInEnergy");
			for (int i = 0; i < outGos.Length; i++)
			{
				outGos[i].SetActive(false);
			}
			GameObject[] inGos = GameObject.FindGameObjectsWithTag("OnlyInCat");
			for (int i = 0; i < inGos.Length; i++)
			{
				inGos[i].SetActive(true);
			}
		}

	
	}

	void ShowUI ()
	{
		txtTotalEnergy.text = "Total Energy: " + totalEnergy.ToString ();
		txtDps.text = "DPS: " + dps.ToString ();
		txtHp.text = "HP: " + hp.ToString ();
		txtEnemy.text = "Enemy: " + enemy.ToString ();
		
		txtTurn.text = "Turns: " + turn.ToString ();
		txtCatTurn.text = "Cat Turns: " + catTurn.ToString ();
		
		txtAttackVal.text = "Attack: " + attackVal.ToString ();
		txtEnergyVal.text = "Energy: " + energyVal.ToString ();
		txtRepairVal.text = "Repair: " + repairVal.ToString ();
		txtEnpowerVal.text = "Enpower: " + enpowerVal.ToString ();

		txtAutoTurn.text = "Auto Turns " + autoEnergyTurn.ToString ();
	}

	void RandomlyHarming ()
	{
		Debug.Log("Attacked by Enemies");
		if (Random.Range (0, 100) < 35) {
			GameManager.hp -= Random.Range(10, 35);
		}
	}
	
	void Attack ()
	{
		GameManager.enemy -= GameManager.dps * Random.Range (0, 10) / 20;
		//GameManager.dps 
	}

	void CheckVal ()
	{
		if (!isCatMode) {

			GameManager.dps = GameObject.Find ("Attack").GetComponent<RoomManager> ().value;
			GameManager.totalEnergy += GameObject.Find ("Energy").GetComponent<RoomManager> ().value * .5f;
			if (GameObject.Find ("Enpower").GetComponent<RoomManager> ().value > GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal) {
				GameObject.Find ("Enpower").GetComponent<RoomManager> ().value -= GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal;
				GameManager.totalEnergy += GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal;
				GameManager.enemy -= 5;
			}	
			if (GameManager.hp < 100) {
				GameManager.hp += 5 * GameObject.Find ("Repair").GetComponent<RoomManager> ().value;
			}
			if (GameManager.hp > 100)
			{
				GameManager.hp = 100;
			}




		} else {
			GameManager.dps = attackVal;
			GameManager.totalEnergy += energyVal * .5f;
			if (enpowerVal > 3) {
				enpowerVal -= 3;
				GameManager.totalEnergy += 3;
				GameManager.enemy -= 5;
			}	
			if (GameManager.hp < 100) {
				GameManager.hp += 5 * repairVal;
			}
			if (GameManager.hp > 100)
			{
				GameManager.hp = 100;
			}


		}
	}

	public void EndTurn ()
	{
		isTurnEnd = true;
		turn++;
	}

	public void EndCatTurn ()
	{
		if (isAttack) {
			if (totalEnergy > 0)
			{
				attackVal++;
				totalEnergy--;
			}
		} else {
			if (attackVal > 0)
			{
				attackVal--;
				totalEnergy++;
			}
		}
		if (isEnergy) {
			if (totalEnergy > 0)
			{
				energyVal++;
				totalEnergy--;
			}
		} else {
			if (energyVal > 0)
			{
				energyVal--;
				totalEnergy++;
			}
		}
		if (isEnpower) {
			if (totalEnergy > 0)
			{
				enpowerVal++;
				totalEnergy--;
			}
		} else {
			if (enpowerVal > 0)
			{
				enpowerVal--;
				totalEnergy++;
			}
		}
		if (isRepair) {
			if (totalEnergy > 0)
			{
				repairVal++;
				totalEnergy--;
			}
		} else {
			if (repairVal > 0)
			{
				repairVal--;
				totalEnergy++;
			}
		}
		isCatTurnEnd = true;
		catTurn++;
	}

	public void CatModeSelection()
	{
		isCatMode = true;

		GamePanel.SetActive (true);
		ModePanel.SetActive (false);

	}

	public void EnergyModeSelection()
	{
		isCatMode = false;

		//autoTurnEndTog.isOn = false;

		GamePanel.SetActive (true);
		ModePanel.SetActive (false);
		
	}
}
