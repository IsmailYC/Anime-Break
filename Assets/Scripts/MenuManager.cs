using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			PlayerPrefs.Save ();
			Application.Quit ();
		}
	}
}
