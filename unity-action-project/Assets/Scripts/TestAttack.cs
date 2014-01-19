using UnityEngine;
using System.Collections;

public class TestAttack : MonoBehaviour {

	public GameObject enemy;

	private Attack myAttack;
	// Use this for initialization
	void Start () {
		myAttack = new Attack(melMin: 7, melMax: 9, melCrit: 20.0f, magMin: 10, magMax: 11, magCrit: 50.0f);
	}
	
	// Update is called once per frame
	void Update () {
		bool fire2 = Input.GetButtonDown("Fire2");

		if (fire2 && enemy)
		{
			enemy.GetComponent<ActorStatus>().receiveAttack(myAttack);
		}	
	}
}
