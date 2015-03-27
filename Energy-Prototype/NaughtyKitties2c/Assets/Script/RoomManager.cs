using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour {

	float value = 0;
	public float minVal = 0;
	public enum Type {Energy, Attack, Repair, Enpower};
	public Type type;
	public Text txtValue;
	public Text txtName;


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
		//if (value > 0.5f)value-= 0.5f;



	}

	public void CheckVal ()
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



			CheckVal ();
			RandomlyHarming ();
			Attack ();
		}

	}

	public void MinusValue ()
	{
		if (value > 0) {
			value--;
			GameManager.totalEnergy++;



			CheckVal ();
			RandomlyHarming ();
			Attack ();
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
}
