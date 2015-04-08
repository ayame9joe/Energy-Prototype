using UnityEngine;
using System.Collections;

public class Progression : MonoBehaviour {

	public Transform start;
	public Transform target;

	public float speed;


	// Use this for initialization
	void Start () {
		this.transform.localPosition = start.localPosition;
	

	}
	
	// Update is called once per frame
	void Update () {

		this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, target.localPosition, speed * Time.deltaTime);




		if (this.transform.localPosition == target.localPosition) {
			Destroy(this.gameObject);
		}


	}
}
