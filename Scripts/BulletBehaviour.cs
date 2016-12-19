using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
    public float speed;

    Rigidbody2D rigidBody;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
        rigidBody.velocity = speed*Vector2.up;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Brick")
            Destroy(gameObject);
    }
}
