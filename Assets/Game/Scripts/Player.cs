using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Triple shot active check
    public bool canTripleShot = false;

    // Speed active check speed boost will be 2x times
    public bool isSpeedBoost = false;

    // Shield Power Up
    public bool shieldSystemOn = false;

    //Player on death animation
    [SerializeField]
    private GameObject PlayerExplosionPrefab;

    // player life
    //[SerializeField]
    public int playerLife = 3;

    // Prefab for the laser sprite
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject shieldGameObject;

    [SerializeField]
    private GameObject[] engines;


    // Projectile fireRate and cooldown settings.
    [SerializeField]
    private float fireRate = 0.15f;
    private float nextFire = 0.0f;

    
    // Player move speed
    [SerializeField]
    private float speed = 5.0f;


    //UI Manager
    private UIManager ui_Manager;
    private GameManager gameManager;
    private SpawnManager Spawn_Manager;
    private AudioSource audiosourcesetting;


    private int hitCount = 0;
	// Use this for initialization
	void Start ()
    {

        // current pos = new position
        transform.position = new Vector3(0, 0, 0);

        ui_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (ui_Manager != null)
        {
            ui_Manager.UpdateLives(playerLife);
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Spawn_Manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (Spawn_Manager != null)
        {
            Spawn_Manager.StartSpawnRoutines();
        }

        audiosourcesetting = GetComponent<AudioSource>();
        hitCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        Movement();

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            LaserShoot();
        }
            
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        if (isSpeedBoost == true) {
            // Code for moving left and right on the X axis
            transform.Translate(Vector3.right * Time.deltaTime * speed * 2 * horizontalInput);
            // Code for moving up and down on the Y Axis
            transform.Translate(Vector3.up * Time.deltaTime * speed * 2 * verticalInput);
        }
        
        else if (isSpeedBoost == false){
            // Code for moving left and right on the X axis
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
            // Code for moving up and down on the Y Axis
            transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        }
        // Restrict player movement on the Y axis so that they do not pass the 0 value or -4.2

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        // Wrapping system so that the player appears to move in a circle on the X axis

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }

        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }

    }

    private void LaserShoot()
    {

        // Projectile instantiation and call of the firerate to create a cooldown to not spam the projectile
        if (Time.time > nextFire)
        {
            audiosourcesetting.Play();
            if (canTripleShot == true)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            nextFire = Time.time + fireRate;

        }
    }

    public void Damage()
    {

        


        if (shieldSystemOn == true)
        {

            shieldSystemOn = false;
            shieldGameObject.SetActive(false);
        }

        else if (shieldSystemOn == false)
        {

            hitCount++;

            if (hitCount == 1)
            {
                engines[0].SetActive(true);
            }
            else if (hitCount == 2)
            {
                engines[1].SetActive(true);
            }

            playerLife--;
            ui_Manager.UpdateLives(playerLife);
            if (playerLife < 1)
            {
                Instantiate(PlayerExplosionPrefab, transform.position, Quaternion.identity);
                gameManager.GameIsOn = false;
                ui_Manager.ShowTitleScreen();
                Destroy(this.gameObject);

            }
        }
    }

        // method to enable the power up

        // enable triple shot power up
    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }


        // enable speed boot power up
    public void SpeedBoostPowerUpOn()
    {
        isSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

        // enable shield power up

    public void ShieldPowerupOn()
    {
        shieldSystemOn = true;
        shieldGameObject.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }

    // coroutine method (ienumerator) to power down

        // Speed boost coroutine
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);

        isSpeedBoost = false;
    }
   
        // triepl shot coroutine
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(10);

        canTripleShot = false;
    }

        // shield coroutine
    public IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(15);
        shieldSystemOn = false;
        shieldGameObject.SetActive(false);
    }
}   
