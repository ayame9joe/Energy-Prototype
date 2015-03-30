using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static float hp = 100;
	public static float dps;

	public static float enemy = 20;

	public static float totalEnergy = 5;

	public static bool isTurnEnd;
	public static float turn;

	public static bool isCatTurnEnd;
	public static float catTurn;

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


	public GameObject panelReplay;

	public static bool isAttack;
	public static bool isEnergy;
	public static bool isRepair;
	public static bool isEnpower;
	// --- Values ---
	public static float attackVal;
	public static float energyVal;
	public static float repairVal;
	public static float enpowerVal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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

	
		if (enemy < 0 || hp < 0) {

			panelReplay.SetActive(true);
		}


	
	}

	void RandomlyHarming ()
	{
		Debug.Log("Attacked by Enemies");
		if (Random.Range (0, 100) < 30) {
			GameManager.hp -= Random.Range(5, 20);
		}
	}
	
	void Attack ()
	{
		GameManager.enemy -= GameManager.dps / (GameManager.dps + 1);
		//GameManager.dps 
	}

	void CheckVal ()
	{


		GameManager.dps = GameObject.Find ("Attack").GetComponent<RoomManager> ().value;
		GameManager.totalEnergy += GameObject.Find ("Energy").GetComponent<RoomManager> ().value * .5f;
		if (GameObject.Find ("Enpower").GetComponent<RoomManager> ().value > GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal)
		{
			GameObject.Find ("Enpower").GetComponent<RoomManager> ().value -= GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal;
			GameManager.totalEnergy += GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal;
			GameManager.enemy -= 5;
		}	
		if (GameManager.hp < 100)
		{
			GameManager.hp += 5 * GameObject.Find ("Repair").GetComponent<RoomManager> ().value;
		}

		/*GameManager.dps = attackVal;
		GameManager.totalEnergy += energyVal * .5f;
		if (enpowerVal > 3)
		{
			enpowerVal -= 3;
			GameManager.totalEnergy += 3;
			GameManager.enemy -= 5;
		}	
		if (GameManager.hp < 100)
		{
			GameManager.hp += 5 * repairVal;
		}*/
	}

	public void EndTurn ()
	{
		isTurnEnd = true;
		turn++;
	}

	public void EndCatTurn ()
	{
		if (isAttack) {
			attackVal++;
		} else {
			attackVal--;
		}
		if (isEnergy) {
			energyVal++;
		} else {
			energyVal--;
		}
		if (isEnpower) {
			enpowerVal++;
		} else {
			enpowerVal--;
		}
		if (isRepair) {
			repairVal++;
		} else {
			repairVal--;
		}
		isCatTurnEnd = true;
		catTurn++;
	}
}
