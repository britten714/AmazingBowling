using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public Rigidbody ball;
    public Transform firePos;
    public Slider powerSlider;
    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public AudioClip chargingClip;
    public float minForce = 15f;
    public float maxForce = 30f;
    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;  //per seconds charging force
    private bool fired;

    private void OnEnable()     //컴포넌트가 enable 될 때마다 매번 실행된다. 
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;

    }

	void Start ()
	{
	    chargeSpeed = (maxForce - minForce) / chargingTime;
	}
	
	void Update ()
	{
	    if (fired == true)
	    {
	        return;
	    }

	    powerSlider.value = minForce;

	    if (currentForce >= maxForce && !fired)
	    {
	        currentForce = maxForce;
            Fire();
	    }
        else if (Input.GetButtonDown("Fire1"))
	    {
            //만약 여기 fired = false 라고 하면 연사가 가능해진다. 이걸 막기 위해 위위 if문을 넣은 것. 
	        currentForce = minForce;

	        shootingAudio.clip = chargingClip;
            shootingAudio.Play();
	    }
	    else if (Input.GetButton("Fire1") && !fired)
	    {
	        currentForce = currentForce + chargeSpeed * Time.deltaTime;

	        powerSlider.value = currentForce;
	    }
        else if (Input.GetButtonUp("Fire1") && !fired)
	    {
	        Fire();
	    }
	}

    private void Fire()
    {
        fired = true;
        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);
        ballInstance.velocity = currentForce * firePos.forward;
        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;
    }
}
