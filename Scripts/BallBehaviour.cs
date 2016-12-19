using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {
	public float speed,freezeTime, glueTime;
	public Transform paddle;
	public GameObject ice;
	public AudioClip ballBounce;

	AudioSource audioSource;
	float timeToDeFreeze, timeToDeGlue;
	bool launched, freezed, glued;
	Rigidbody2D rb;
    Vector2 startPos, endPos;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
        startPos = endPos = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		switch (GameManager.gm.gameState) {
		case GameManager.GameStates.Play:
			if (freezed) {
				if (Time.time > timeToDeFreeze) {
					freezed = false;
					ice.SetActive (false);
				}
			}
            if (glued)
            {
                if(Time.time>timeToDeGlue)
                {
                        glued = false;
                }
            }
			#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR
			if (!launched) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					Launch();
				}
			}
#else
			if (!launched) {
				if (Input.touchCount > 0)
                {
                    Touch lastTouch = Input.touches[Input.touchCount - 1];
                    if (lastTouch.phase == TouchPhase.Began)
                        startPos = Camera.main.ScreenToWorldPoint(lastTouch.position);
                    else if (lastTouch.phase == TouchPhase.Ended)
                    {
                        endPos = Camera.main.ScreenToWorldPoint(lastTouch.position);
                        if ((endPos.y - startPos.y) > 0.1f)
                            Launch();
                    }
                }
			}
#endif
                break;
		case GameManager.GameStates.Win:
			rb.velocity = Vector2.zero;
			break;
		}
	}

	public void Launch()
	{
		transform.parent = null;
		int dir = Random.Range(0,2);
		if (dir == 0)
			dir = -1;
		rb.AddForce(dir*speed*Vector2.one);
		launched = true;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "BottomWall") {
			if (GameManager.gm.LoseLife ()) {
				rb.velocity = Vector2.zero;
				transform.parent = paddle;
				transform.localPosition = new Vector3 (0f, 0.3775f, 0f);
				launched = false;
			} else {
				Destroy (gameObject);
			}
		} else {
            if(glued)
            {
                if(coll.gameObject.tag=="Player")
                {
                    rb.velocity = Vector2.zero;
                    transform.parent = paddle;
                    launched = false;
                }
            }
			audioSource.PlayOneShot(ballBounce);
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (launched) {
			Vector2 velocity;
			if (rb.velocity.x==0) {
				float magnitude = rb.velocity.magnitude;
				int dir = Random.Range (0, 2);
				if (rb.velocity.y > 0) {
					if (dir == 0)
						velocity = new Vector2 (magnitude * 0.1f, magnitude * 0.995f);
					else
						velocity = new Vector2 (magnitude * -0.1f, magnitude * 0.995f);
				} else {
					if (dir == 0)
						velocity = new Vector2 (magnitude * 0.1f, magnitude * -0.995f);
					else
						velocity = new Vector2 (magnitude * -0.1f, magnitude * -0.995f);
				}
				rb.velocity = velocity;
			} else if (Mathf.Abs(rb.velocity.y) < 0.5f) {
				float magnitude = rb.velocity.magnitude;
				int dir = Random.Range (0, 2);
				if (rb.velocity.x > 0) {
					if (dir == 0)
						velocity = new Vector2 (magnitude * 0.966f, magnitude * 0.088f);
					else
						velocity = new Vector2 (magnitude * 0.966f, magnitude * -0.088f);
				} else {
					if (dir == 0)
						velocity = new Vector2 (magnitude * -0.966f, magnitude * 0.088f);
					else
						velocity = new Vector2 (magnitude * -0.966f, magnitude * -0.088f);
				}
				rb.velocity = velocity;
			}
		}
	}

	public void Freeze()
	{
        if (freezed)
        {
            timeToDeFreeze += freezeTime;
        }
        else {
            freezed = true;
            timeToDeFreeze = Time.time+freezeTime;
            ice.SetActive(true);
        }
	}

    public void Glue()
    {
        if (glued)
        {
            timeToDeGlue += glueTime;
        }
        else {
            glued = true;
            timeToDeGlue = Time.time + glueTime;
        }
    }
}