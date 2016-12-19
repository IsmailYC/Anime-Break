using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageManager : MonoBehaviour {
	public Sprite[] images;
	public RectTransform content, imageContent;
	public GameObject button, imageDisplay;

    int unlockedLevel;
	// Use this for initialization
	void Start () {
		unlockedLevel = PlayerPrefsManager.GetUnlockedLevel();
		content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, unlockedLevel * 210+10);
		content.anchoredPosition = Vector2.zero;
		for (int i = 0; i < unlockedLevel; i++) {
			GameObject tempBtn = (GameObject)Instantiate (button);
			RectTransform tempRectTrans = tempBtn.GetComponent<RectTransform> ();
			tempRectTrans.SetParent (content);
			tempRectTrans.anchoredPosition = new Vector2 (0, -10-210*i);
			tempRectTrans.localScale = Vector3.one;
			tempBtn.GetComponent<Image> ().sprite = images [i];
			LoadImage tempLoader = tempBtn.GetComponentInChildren<LoadImage> ();
			tempLoader.image = images [i];
			tempLoader.content = imageContent;
			tempLoader.display = imageDisplay;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (imageDisplay.activeSelf) {
			if (Input.GetButtonDown ("Cancel"))
				imageDisplay.SetActive (false);
		} else {
			if (Input.GetButtonDown ("Cancel"))
				SceneManager.LoadScene ("Menu");
		}
	}
}
