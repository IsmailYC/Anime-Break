using UnityEngine;
using System.Collections;

public class ZoomBehaviour : MonoBehaviour {
	public int limit=8;

	float width,height;
    int scale=1;
	Vector2 startPos1,startPos2,endPos1,endPos2;
	RectTransform content;
	bool secondTouch;
	// Use this for initialization
	void Awake () {
		content = GetComponent<RectTransform> ();
		width = content.rect.size.x;
        height = content.rect.size.y;
        endPos1 = Vector2.zero;
		endPos2 = Vector2.zero;
		startPos1 = Vector2.zero;
		startPos2 = Vector2.zero;
		secondTouch = false;
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.Plus) || Input.GetKeyDown (KeyCode.KeypadPlus)) {
			if (scale<limit) {
                scale++;
				content.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, width * scale);
				content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, height * scale);
			}
		} else if (Input.GetKeyDown (KeyCode.Minus) || Input.GetKeyDown (KeyCode.KeypadMinus)) {
			if (scale>1) {
                scale--;
				content.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, width * scale);
				content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, height * scale);
			}
		}
#else
		if(Input.touchCount==1)
		{
			if(Input.touches[0].phase == TouchPhase.Began)
				startPos1= Input.touches[0].position;
			if(Input.touches[0].phase == TouchPhase.Ended)
			{
				endPos1= Input.touches[0].position;
				if(secondTouch)
				{
					Vector2 startDistance= startPos2-startPos1;
					Vector2 endDistance= endPos2-endPos1;
					if(startDistance.magnitude>endDistance.magnitude)
					{
						if (scale>1) {
                            scale--;
				            content.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, width * scale);
				            content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, height * scale);
		            	}
					}
					else
					{
						if (scale<limit) {
                            scale++;
		            		content.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, width * scale);
				            content.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, height * scale);
            			}
					}
					secondTouch= false;
				}
			}
		}
		if(Input.touchCount==2)
		{
			if(Input.touches[1].phase == TouchPhase.Began)
				startPos2= Input.touches[1].position;
			if(Input.touches[1].phase == TouchPhase.Ended)
			{
				endPos2= Input.touches[1].position;
				secondTouch= true;
			}
		}
#endif
    }
}