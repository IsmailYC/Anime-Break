using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour {
	public string[] levels;
	public RectTransform content;
	public GameObject button;

	int lastLevel;
	// Use this for initialization
	void Start () {
		lastLevel = PlayerPrefsManager.GetLastLevel ();
		content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, lastLevel * 100);
		content.anchoredPosition = Vector2.zero;
		for (int i = 0; i < lastLevel; i++) {
			GameObject tempBtn = (GameObject)Instantiate (button);
			RectTransform tempRectTrans = tempBtn.GetComponent<RectTransform> ();
			tempRectTrans.SetParent (content);
			tempRectTrans.anchoredPosition = new Vector2 (0, 0-100*i);
			tempRectTrans.localScale = Vector3.one;
			tempBtn.GetComponentInChildren<Text> ().text = levels [i];
			tempBtn.GetComponentInChildren<LoadScene> ().scene = levels [i];
		}
	}

	void Update()
	{
		if (Input.GetButtonDown ("Cancel"))
			SceneManager.LoadScene ("Menu");
	}
}
