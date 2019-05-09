using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour {
    public GameObject bulletPrefab;

	// Use this for initialization
	void OnEnable()
    {
        Invoke("Shoot", 0.0f);
    }

    void Shoot()
    {
        GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position-0.5f*Vector3.right+Vector3.up, Quaternion.identity);
        GameObject bullet2 = (GameObject)Instantiate(bulletPrefab, transform.position + 0.5f * Vector3.right+Vector3.up, Quaternion.identity);
        if (gameObject.activeSelf)
            Invoke("Shoot", 0.25f);
    }
}
