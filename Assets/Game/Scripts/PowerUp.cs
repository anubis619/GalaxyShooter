using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private int powerupID; // 0 = triple shot, 1 = speed boost, 2 = shield
    
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.down *speed * Time.deltaTime);

        // destroy powerup if position is -6 on the Y Axis. 
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

    }

    // collision triggering of the powerup with a colissionBox enabled object
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            // access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                
                if (powerupID == 0)
                {
                    // enable the triple shot bool
                    player.TripleShotPowerUpOn();
                }
                else if (powerupID == 1)
                {
                    // enable speed boost
                    player.SpeedBoostPowerUpOn();
                }

                else if (powerupID == 2) {
                    // enable shields

                }


            }

            // destroy object
            Destroy(this.gameObject);

        }

        
        
    }
}
