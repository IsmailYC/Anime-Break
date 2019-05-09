using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public int translationSpeed;
	public float Kp,epsilon,scaleTime,minimizeTime, shootTime;
	public Transform rightEdge, leftEdge;
    public GameObject shooter;
	//public GameObject ball;

	float xMax,xMin,timeToDeScale,timeToDeMinimize,timeToDeShoot;
	Vector3 mousePos;
	Vector3 minScale= new Vector3(0.75f,1.0f,1.0f);
	Vector3 maxScale= new Vector3(1.5f, 1.0f, 1.0f);
    bool scaled, minimized, shooting;
	// Use this for initialization
	void Start () {
		xMax = rightEdge.position.x;
		xMin = leftEdge.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        if(shooting)
        {
            if(Time.time>timeToDeShoot)
            {
                shooting = false;
                shooter.SetActive(false);
            }
        }
		if (scaled) { 
			if (Time.time>timeToDeScale) {
				scaled = false;
				transform.localScale = Vector3.one;
				xMax += 0.375f;
				xMin += -0.375f;
			}
		}
		else if (minimized) {
			if (Time.time>timeToDeMinimize) {
                minimized = false;
				transform.localScale = Vector3.one;
				xMax -= 0.1875f;
				xMin -= -0.1875f;
			}
		}
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR || UNITY_WEBGL
        if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x> xMin)
             transform.Translate(-translationSpeed * Time.deltaTime * Vector2.right);
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < xMax)
            transform.Translate(translationSpeed * Time.deltaTime * Vector2.right);
#else
		if (Input.touchCount > 0)
			mousePos = Camera.main.ScreenToWorldPoint (Input.touches [Input.touchCount - 1].position);
        else
            mousePos= Vector3.zero;
        if(mousePos.x<0 && transform.position.x> xMin)
             transform.Translate(-translationSpeed * Time.deltaTime * Vector2.right);
        if (mousePos.x>0 && transform.position.x < xMax)
             transform.Translate(translationSpeed * Time.deltaTime * Vector2.right);
#endif
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Scale") {
			Destroy (coll.gameObject);
			ScalePaddle ();
		} else if (coll.gameObject.tag == "Freeze") {
			Destroy (coll.gameObject);
			FreezeBall ();
		} else if (coll.gameObject.tag == "Double") {
			Destroy (coll.gameObject);
			StartCoroutine(DoubleBall ());
		} else if (coll.gameObject.tag == "Minimize") {
			Destroy (coll.gameObject);
			MinimizePaddle ();
		}else if(coll.gameObject.tag=="Shoot")
        {
            Destroy(coll.gameObject);
            StartShooting();
        }else if(coll.gameObject.tag=="Glue")
        {
            Destroy(coll.gameObject);
            GlueBall();
        }
	}

	void ScalePaddle()
	{
		if (minimized) {
			minimized = false;
			transform.localScale = Vector3.one;
			xMax -= 0.1875f;
			xMin -= -0.1875f;
		} else if (scaled) 
			timeToDeScale += scaleTime;
		else{
			scaled = true;
			timeToDeScale = Time.time+scaleTime;
			transform.localScale = maxScale;
			xMax -= 0.375f;
			xMin -= -0.375f;
		}
	}

	void MinimizePaddle()
	{
		if (scaled) {
			scaled = false;
			transform.localScale = Vector3.one;
			xMax += 0.375f;
			xMin += -0.375f;
		} else if (minimized) 
			timeToDeMinimize += minimizeTime;
		else{
			minimized = true;
			timeToDeMinimize = Time.time+minimizeTime;
			transform.localScale = minScale;
			xMax += 0.1875f;
			xMin += -0.1875f;
		}
	}

    void StartShooting()
    {
        if(shooting)
        {
            timeToDeShoot += shootTime;
        }
        else
        {
            shooting = true;
            timeToDeShoot = Time.time + shootTime;
            shooter.SetActive(true);
        }
    }

	IEnumerator DoubleBall()
	{
		GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
		GameObject childBall = (GameObject)Instantiate (ball);
		childBall.GetComponent<BallBehaviour> ().paddle = transform;
		childBall.transform.parent = transform;
		childBall.transform.localPosition = new Vector3 (0f, 0.3775f, 0f);
		GameManager.gm.balls++;
		yield return new WaitForSeconds (0.1f);
		childBall.GetComponent<BallBehaviour> ().Launch ();
	}

	void FreezeBall()
	{
		GameObject[] balls = GameObject.FindGameObjectsWithTag ("Ball");
		foreach (GameObject ball in balls) {
			ball.GetComponent<BallBehaviour> ().Freeze ();
		}
	}

    void GlueBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<BallBehaviour>().Glue();
        }
    }
}
