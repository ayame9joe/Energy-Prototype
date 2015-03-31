using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour {

	// --- Val ---
	public float value = 0;
	public float minVal = 0;

	// --- Type ---
	public enum Type {Energy, Attack, Repair, Enpower};
	public Type type;

	// --- UI ---
	public Text txtValue;
	public Text txtName;
	public Text txtLevel;

	bool isLevelingUp = false;
	float level = 0;

	public Toggle catToggle;

	
	// Update is called once per frame
	void Update () {

		ShowUI ();

		CheckCats ();




	}

	void ShowUI ()
	{
		if (minVal > 0) {
			txtValue.text = value.ToString () + "/" + minVal.ToString ();
		} else {
			txtValue.text = value.ToString ();
		}

		txtName.text = type.ToString ();
		
		txtLevel.text = "Level: " + level.ToString ();

	}

	void CheckCats()
	{
		if (catToggle.isOn) {
			switch (type) {
			case Type.Attack:
				GameManager.isAttack = true;
				break;
			case Type.Energy:
				GameManager.isEnergy = true;
				break;
			case Type.Enpower:
				GameManager.isEnpower = true;
				break;
			case Type.Repair:
				GameManager.isRepair = true;
				break;
			}
		} else {
			switch (type) {
			case Type.Attack:
				GameManager.isAttack = false;
				break;
			case Type.Energy:
				GameManager.isEnergy = false;
				break;
			case Type.Enpower:
				GameManager.isEnpower = false;
				break;
			case Type.Repair:
				GameManager.isRepair = false;
				break;
			}
			
		}
	}

	void CheckVal ()
	{
		switch (type) {
		case Type.Attack:
			GameManager.dps = value;
			break;
		case Type.Energy:
			GameManager.totalEnergy += value * .5f;
			break;
		case Type.Enpower:
			if (value > minVal)
			{
				value -= minVal;
				GameManager.totalEnergy += minVal;
				GameManager.enemy -= 5;
			}
			break;
		case Type.Repair:
			if (GameManager.hp < 100)
			{
				GameManager.hp += 5 * value;
			}
			break;
		}
	}

	public void AddValue ()
	{
		if (GameManager.totalEnergy > 0) {
			value++;
			GameManager.totalEnergy--;

			if (GameManager.autoEndTurn) {
				CheckVal();
				RandomlyHarming();
				Attack();
				GameManager.autoEnergyTurn++;
			}
		}



	}

	public void MinusValue ()
	{
		if (value > 0) {
			value--;
			GameManager.totalEnergy++;

			if (GameManager.autoEndTurn) {
				CheckVal();
				RandomlyHarming();
				Attack();
				GameManager.autoEnergyTurn++;
			}
		}
	}

	public void LvUp ()
	{
		GameManager.totalEnergy -= this.value;

		level += this.value;
		for (int i = 0; i < level + 1; i++) {
			CheckVal ();

			//--Todo：玩家不能操作；效能提升
		}
	}

	void RandomlyHarming ()
	{
		//Debug.Log("Attacked by Enemies");
		if (Random.Range (0, 100) < 30) {
			GameManager.hp -= Random.Range(5, 20);
		}
	}
	
	void Attack ()
	{
		GameManager.enemy -= GameManager.dps / (GameManager.dps + 1);
		//GameManager.dps 
	}







}
