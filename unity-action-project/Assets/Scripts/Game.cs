using UnityEngine;
using System.Collections;

namespace Game 
{
	public class Game : MonoBehaviour
	{
		public Vector3 gravity;

		void Awake() 
		{
			if (gravity.magnitude != 0)
			{
				Physics.gravity = gravity;
			}
		}

		void Start ()
		{
		}
		
		void Update ()
		{
		}
	}
}