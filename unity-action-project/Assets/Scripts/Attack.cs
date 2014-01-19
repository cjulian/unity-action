using UnityEngine;
using System.Collections;

public class Attack {

	public int meleeMin;
	public int meleeMax;
	public float meleeCritChance;

	public int magicMin;
	public int magicMax;
	public float magicCritChance;

	public int poisonDmg;
	public int poisonDuration; // number of times poison damage is applied (approximately 1 time/sec)
	public float poisonChance;

	public Attack(int melMin = 0,
				  int melMax = 0,
				  float melCrit = 0.0f,

				  int magMin = 0,
				  int magMax = 0,
				  float magCrit = 0.0f,

				  int pDmg = 0,
				  int pDur = 0,
				  float pChance = 0.0f)
	{
		meleeMin = melMin;
		meleeMax = melMax;
		meleeCritChance = melCrit;
		magicMin = magMin;
		magicMax = magMax;
		magicCritChance = magCrit;
		poisonDmg = pDmg;
		poisonChance = pChance;
		poisonDuration = pDur;
	}
}
