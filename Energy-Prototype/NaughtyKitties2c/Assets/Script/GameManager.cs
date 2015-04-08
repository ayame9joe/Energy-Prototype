using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// --- Vals ---
	public static float hp = 100;
	public static float dps;
	public static float enemy = 20;
	public static float tempEnergy = 5;

	public static float attackVal;
	public static float energyVal;
	public static float repairVal;
	public static float enpowerVal;

	public static bool isTurnEnd;
	public static float turn;

	public static bool isCatTurnEnd;
	public static float catTurn;

	public static float autoEnergyTurn;

	public static float numOfCats;

	float totalEnergyVal;

	//public static float autoCatTurn;

	// --- UI ---

	public Text txttempEnergy;
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

	public Text txtBeingAttacked;
	public Text txtHarming;
	public Text txtEncharging;
	public Text txtEnpower;

	public Text txtResult;

	public Image imgUs;
	public Image imgEnemy;

	//--- Boolen ---
	public static bool isAttack;
	public static bool isEnergy;
	public static bool isRepair;
	public static bool isEnpower;


	public static bool isCatMode;
	public static bool autoEndTurn;

	bool textClear;

	//--- Panels ---
	public GameObject ModePanel;
	public GameObject GamePanel;
	public GameObject ReplayPanel;

	// Use this for initialization
	void Start () {

		GamePanel.SetActive (false);

		txtBeingAttacked.text = "";
		txtHarming.text = "";
		txtEnpower.text = "";
		txtEncharging.text = "";
	
	}
	
	// Update is called once per frame
	void Update () {

		if (textClear) {
			textClear = false;
			txtResult.text.IsNormalized();
		}

		numOfCats = GameObject.FindGameObjectsWithTag ("Cat").Length;
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

				/*if (tempEnergy < totalEnergyVal && totalEnergyVal > 1) {
					totalEnergyVal--;
				}*/

				imgUs.rectTransform.sizeDelta = new Vector2 (
					imgUs.rectTransform.sizeDelta.x, hp / 100 * 100);
				imgEnemy.rectTransform.sizeDelta = new Vector2 (
					imgEnemy.rectTransform.sizeDelta.x, enemy / 20 * 100);

				if (isAttack) {
					
					//attackVal = tempEnergy / numOfCats;
					if (tempEnergy > 0)
					{
						//GameObject go = GameObject.Instantiate((1.0f/numOfCats).ToString(), GameObject.Find("Total Energy").transform.position, this.transform.localRotation) as GameObject;
						//go.transform.position = Vector3(go.transform.position, GameObject.Find("Attack").transform.position, 100 * Time.deltaTime);

						
						if (numOfCats > 1)
						{
							attackVal += 1.0f/numOfCats;
							tempEnergy -= 1.0f/numOfCats;
						}
					}
				} else {
					if (attackVal > 0)
					{
						if (numOfCats > 1)
						{
							attackVal -= 1.0f/numOfCats;
							tempEnergy += 1.0f/numOfCats;
						}
					}
				}
				if (isEnergy) {
					if (tempEnergy > 0)
					{
						txtEncharging.text = "Encharge energy with " + 1.0f/numOfCats + " points";
						txtResult.text = "";
						textClear = true;
						txtResult.text = "Encharge energy with " + 1.0f/numOfCats + " points";
						
						if (numOfCats > 1)
						{
							energyVal += 1.0f/numOfCats;
							tempEnergy -= 1.0f/numOfCats;
						}
					}
				} else {
					if (energyVal > 0)
					{
						if (numOfCats > 1)
						{
							energyVal -= 1.0f/numOfCats;
							tempEnergy += 1.0f/numOfCats;
						}
					}
				}
				if (isEnpower) {
					if (tempEnergy > 0)
					{
						
						if (numOfCats > 1)
						{
							enpowerVal += 1.0f/numOfCats;
							tempEnergy -= 1.0f/numOfCats;
						}
					}
				} else {
					if (enpowerVal > 0)
					{
						if (numOfCats > 1)
						{
							enpowerVal -= 1.0f/numOfCats;
							tempEnergy += 1.0f/numOfCats;
						}
					}
				}
				if (isRepair) {
					if (tempEnergy > 0)
					{
						
						if (numOfCats > 1)
						{
							repairVal += 1.0f/numOfCats;
							tempEnergy -= 1.0f/numOfCats;
						}
					}
				} else {
					if (repairVal > 0)
					{
						if (numOfCats > 1)
						{
							repairVal -= 1.0f/numOfCats;
							tempEnergy += 1.0f/numOfCats;
						}
					}
				}
				
				
				totalEnergyVal += energyVal * .5f;

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

		//if (tempEnergy > totalEnergyVal || tempEnergy == totalEnergyVal) {
			txttempEnergy.text = "Total Energy: " + tempEnergy  + " = 5 + " + (totalEnergyVal).ToString () + " - " + (enpowerVal + energyVal + repairVal + attackVal).ToString();
		//} else {
		//	txttempEnergy.text = "Total Energy: " + 0.ToString () + " + " + (tempEnergy).ToString ();
		//}

			
			//(tempEnergy - totalEnergyVal * .5f).ToString() + " + " + (totalEnergyVal * .5f).ToString();
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
			float temp = Random.Range(10, 35);
			txtBeingAttacked.text = "Harmed by enemies for " + temp.ToString() + " points";
			txtResult.text = "";
			textClear = true;
			txtResult.text = "Harmed by enemies for " + temp.ToString() + " points";

			GameManager.hp -= temp;
		}
	}
	
	void Attack ()
	{
		float temp = Random.Range (5, 10) / 8;
		GameManager.enemy -= GameManager.dps * temp;
		txtHarming.text = "Attack enemies for " + dps * temp + " points";
		txtResult.text = "";
		textClear = true;
		txtResult.text = "Attack enemies for " + dps * temp + " points";
	}

	void CheckVal ()
	{
		if (!isCatMode) {

			GameManager.dps = GameObject.Find ("Attack").GetComponent<RoomManager> ().value;
			GameManager.tempEnergy += GameObject.Find ("Energy").GetComponent<RoomManager> ().value * .5f;
			if (GameObject.Find ("Enpower").GetComponent<RoomManager> ().value > GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal) {
				GameObject.Find ("Enpower").GetComponent<RoomManager> ().value -= GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal;
				GameManager.tempEnergy += GameObject.Find ("Enpower").GetComponent<RoomManager> ().minVal;
				GameManager.enemy -= 5;
				txtEnpower.text = "Attack enemies with bonus: 5 points!";
				txtResult.text = "";
				textClear = true;
				txtResult.text = "Attack enemies with bonus: 5 points!";
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
			GameManager.tempEnergy += energyVal * .5f;
			if (enpowerVal > 3) {
				enpowerVal -= 3;
				GameManager.tempEnergy += 3;
				GameManager.enemy -= 5;
				txtEnpower.text = "Attack enemies with bonus: 5 points!";
				txtResult.text = "";
				textClear = true;
				txtResult.text = "Attack enemies with bonus: 5 points!";
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


		isCatTurnEnd = true;
		catTurn++;


	}

	public void CatModeSelection()
	{
		isCatMode = true;
		//autoTurnEndTog.isOn = false;
		autoEndTurn = false;

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
