using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TracksManager : MonoBehaviour {
	public string[] clips;
	public string[] titles;
	public GameObject button;
	public RectTransform content;

	AudioSource player;
	int unlockedLevel;
	// Use this for initialization
	void Start () {
		player = GetComponent<AudioSource> ();
		unlockedLevel = PlayerPrefsManager.GetUnlockedLevel ();
		content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, unlockedLevel * 70);
		content.anchoredPosition = Vector2.zero;
		for (int i = 0; i < unlockedLevel; i++) {
			GameObject tempBtn = (GameObject)Instantiate (button);
			RectTransform tempRectTrans = tempBtn.GetComponent<RectTransform> ();
			tempRectTrans.SetParent (content);
			tempRectTrans.anchoredPosition = new Vector2 (0, 0-70*i);
			tempRectTrans.localScale = Vector3.one;
			tempBtn.GetComponentInChildren<Text> ().text = titles [i];
			tempBtn.GetComponentInChildren<LoadClip> ().player = player;
			tempBtn.GetComponentInChildren<LoadClip> ().clip = clips [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel"))
			SceneManager.LoadScene ("Menu");
	}
}
