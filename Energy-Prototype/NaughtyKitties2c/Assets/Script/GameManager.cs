using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static float hp = 100;
	public static float dps;

	public static float enemy = 20;

	public static float totalEnergy = 5;


	// --- UI ---

	public Text txtTotalEnergy;
	public Text txtDps;
	public Text txtHp;
	public Text txtEnemy;

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

	
		if (enemy < 0 || hp < 0) {
			panelReplay.SetActive(true);
		}
	
	}
}
