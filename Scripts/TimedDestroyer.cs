using UnityEngine;
using System.Collections;

public class TimedDestroyer : MonoBehaviour {
	public float deltaTime;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, deltaTime);
	}
}
