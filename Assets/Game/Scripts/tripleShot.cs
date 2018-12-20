using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripleShot : MonoBehaviour {

    [SerializeField]
    private float speed = 10.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        transform.Translate(Vector3.up * speed * Time.deltaTime);
        // Destroy triple Shot  prefab when going out of screen.

        if (transform.position.y > 6)
        {
            Destroy(this.gameObject);
        }
    }
}
