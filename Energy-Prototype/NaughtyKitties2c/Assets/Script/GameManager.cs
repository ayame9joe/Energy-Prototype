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

	// --- UI ---

	public Text txtTotalEnergy;
	public Text txtDps;
	public Text txtHp;
	public Text txtEnemy;
	public Text txtTurn;




	public GameObject panelReplay;
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

		if (isTurnEnd) {

			CheckVal();
			RandomlyHarming();
			Attack();
			isTurnEnd = false;
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
		//Debug.Log(GameObject.Find ("Attack").GetComponent<RoomManager> ().value);
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
	}

	public void EndTurn ()
	{
		isTurnEnd = true;
		turn++;
	}
}
