using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour {
	public Sprite image;
	public RectTransform content;
	public GameObject display;

	public void Load()
	{
		Vector2 spriteSize = image.bounds.size;
		content.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 480);
		content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 480 * spriteSize.y / spriteSize.x);
		content.GetComponentInChildren<Image> ().sprite = image;
		display.SetActive (true);
	}
}
