using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    //variable for the speed
    [SerializeField]
    private float speed = 2f;


    // Prefab for the explosion
    [SerializeField]
    private GameObject EnemyExplosionPrefab;

    // UI Manager
    private UIManager UIManager;
    [SerializeField]
    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //when of the screen on the bottom

        if(transform.position.y < -6.33f)
        {
            // respawn back on top with a new x position between the bouds of the screen
            float xLocation = Random.Range(-8f, 8f);
            transform.position = new Vector3(xLocation, 7, 0);
            
        }


        

    }

    // collision triggering of the powerup with a colissionBox enabled object
    // Destroy enemy on hit with either the Laser tag object or the Player tag object
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.CompareTag("Laser"))
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            GameObject explosionPrefab = Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);
            explosionPrefab.GetComponent<DestroyDelay>().DestroyObject();
            UIManager.UpdateScore();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            
        }
        else if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            
        }
    }
   
}
