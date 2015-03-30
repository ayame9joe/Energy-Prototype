using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour {

	public float value = 0;
	public float minVal = 0;
	public enum Type {Energy, Attack, Repair, Enpower};
	public Type type;
	public Text txtValue;
	public Text txtName;
	public Text txtLevel;

	bool isLevelingUp = false;
	float level = 0;
	public Toggle newToggle;

	// Use this for initialization
	void Start () {

		switch (type) {
		case Type.Attack:

			break;
		case Type.Energy:

			break;
		case Type.Enpower:

			
			break;
		case Type.Repair:
			
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (minVal > 0) {
			txtValue.text = value.ToString () + "/" + minVal.ToString ();
		} else {
			txtValue.text = value.ToString ();
		}

		txtName.text = type.ToString ();

		txtLevel.text = "Level: " + level.ToString ();
		//if (value > 0.5f)value-= 0.5f;




	}

	void CheckVal ()
	{
		/*if (value > minVal) {
			
			switch (type) {
			case Type.Attack:
				GameManager.dps++;
				break;
			case Type.Energy:
				GameManager.totalEnergy+=2;
				break;
			case Type.Enpower:
				
				break;
			case Type.Repair:
				GameManager.hp++;
				break;
			}
			
		} else {
			switch (type) {
			case Type.Attack:
				if (GameManager.dps > 1)
				{
					GameManager.dps--;
				}
				break;
			case Type.Energy:
				GameManager.totalEnergy-= 2;
				break;
			case Type.Enpower:
				
				break;
			case Type.Repair:
				GameManager.hp++;
				break;
			}
		}*/
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



		}

	}

	public void MinusValue ()
	{
		if (value > 0) {
			value--;
			GameManager.totalEnergy++;



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


	public void MewChanged (bool isPut)
	{
		if (newToggle.isOn) {
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
				if (GameManager.attackVal > 0)
				{
					GameManager.isAttack = false;
				}
				break;
			case Type.Energy:
				if (GameManager.energyVal > 0)
				{
					GameManager.isEnergy = false;
				}
				break;
			case Type.Enpower:
				if (GameManager.enpowerVal > 0)
				{
					GameManager.isEnpower = false;
				}
				break;
			case Type.Repair:
				if (GameManager.repairVal > 0)
				{
					GameManager.isRepair = false;
				}
				break;
			}
		
		}
	}




}
