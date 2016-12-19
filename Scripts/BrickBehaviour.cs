using UnityEngine;
using System.Collections;

public class BrickBehaviour : MonoBehaviour {
	public GameObject[] children;
	public GameObject iceBlock;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            Destroy(coll.gameObject);
            int index = Random.Range(0, children.Length);
            if (children[index] == null)
            {
                GameManager.gm.CollectBrick();
                Destroy(gameObject);
            }
            else {
                GameObject child = (GameObject)Instantiate(children[index], transform.position, transform.rotation);
                if (child.tag == "Brick")
                {
                    child.transform.parent = transform.parent;
                    Destroy(gameObject);
                }
                else {
                    GameManager.gm.CollectBrick();
                    Destroy(gameObject);
                }
            }
        }
    }

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ball") {
			int index = Random.Range (0, children.Length);
			if (children [index] == null) {
				GameManager.gm.CollectBrick ();
				Destroy (gameObject);
			} else {
				GameObject child = (GameObject)Instantiate (children[index], transform.position, transform.rotation);
				if (child.tag == "Brick") {
					child.transform.parent = transform.parent;
					Destroy (gameObject);
				} else {
					GameManager.gm.CollectBrick ();
					Destroy (gameObject);
				}
			}
		}
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Ice") {
			GameManager.gm.CollectBrick ();
			Destroy (gameObject);
			GameObject child = (GameObject)Instantiate (iceBlock, transform.position, transform.rotation);
			child.transform.parent = transform.parent;
		}
	}
}