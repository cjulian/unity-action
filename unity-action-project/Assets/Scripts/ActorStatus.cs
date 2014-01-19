using UnityEngine;
using System.Collections;

public class ActorStatus : MonoBehaviour {

	// primary stats
	public int healthMax = 100;
	public int manaMax = 100;
	public int staminaMax = 100;

	private int health = 100;
	private int mana = 100;
	private int stamina = 100;

	// resistance levels
	public float resMelee = 0.0f;
	public float resMagic = 0.0f;
	public float resPoison = 0.0f;
	public float resCrit = 0.0f;

	// status conditions
	private bool poisoned = false;
	private int poisonDamage = 0;
	private int poisonDuration = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Reduce health by damage.  Can possibly be negative (ie. INCREASES health) if
	// player resistances are high enough.
	//
	// TODO: display damage taken on screen with color based on damage type
	private void takeDamage(int damage, DamageType type)
	{
		health -= damage;

		if (damage < 0) {
			// green text to show health up
		} else {
			// text color corresponding to damage type
		}
		Debug.Log("enemy takes " + damage + " " + type.ToString() + " damage!\nHP = " + health + "/" + healthMax);
	}

	// Calculates the amount of damage taken by an attack
	public void receiveAttack(Attack attack) {
		// calculate base melee and magic damage from given min/max ranges and actor resistance
		float meleeDmg = Random.Range(attack.meleeMin, attack.meleeMax + 1) * (1.0f - resMelee/100);
		float magicDmg = Random.Range(attack.magicMin, attack.magicMax + 1) * (1.0f - resMagic/100);

		// calculate critical chance based on given percentage and actor critical resistance
		float meleeCritChance = attack.meleeCritChance * (1.0f - resCrit/100);
		float magicCritChance = attack.magicCritChance * (1.0f - resCrit/100); // TODO: magic critical resistance should be based on some other stats

		// use previously calculated critical chance and roll to see if critical damage modifier should be applied
		bool isMelCritical = (Random.Range(0.0f, 100.0f) < meleeCritChance) ? true : false;
		bool isMagCritical = (Random.Range(0.0f, 100.0f) < magicCritChance) ? true : false;

		if (isMelCritical) {
			meleeDmg *= 1.6f; // TODO: critical damage rate shouldn't be hardcoded
		}
		if (isMagCritical) {
			magicDmg *= 1.4f; // TODO: critical damage rate shouldn't be hardcoded
		}

		int intMeleeDmg = (int)Mathf.Round(meleeDmg);
		int intMagicDmg = (int)Mathf.Round(magicDmg);

		// make appropriate calls to takeDamage() to actually change the actors HP according to the attack damage.
		if (intMeleeDmg > 0) {
			if (isMelCritical) {
				takeDamage((int)meleeDmg, DamageType.melCritical);
			} else {
				takeDamage((int)meleeDmg, DamageType.melee);
			}
		}
		if (intMagicDmg > 0) {
			if (isMagCritical) {
				takeDamage((int)magicDmg, DamageType.magCritical);
			} else {
				takeDamage((int)magicDmg, DamageType.magic);
			}
		}
	}

	public void blockAttack() {
	}
}

// Enum for passing damage type
public enum DamageType {
	melee,
	melCritical,
	magic,
	magCritical,
	poison
}