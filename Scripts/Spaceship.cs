using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	public ParticleSystem flameEmitter;
	private bool detectCollision;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(CheckOffScreen());
		StartCoroutine(FileBullet());
		StartCoroutine(enableCollision());
		flameEmitter.Stop();
		detectCollision = false;
    }
		public void ResetShip(){
			transform.position = new Vector3();
		}
    // Update is called once per frame
    void FixedUpdate()
    {
		if (flameEmitter.isPlaying)
			flameEmitter.Stop();

        if (Input.GetKey("up"))
		{
			GetComponent<Rigidbody>().AddForce(transform.forward * 8f);
			flameEmitter.Play();
		}
		else
		{
			if (flameEmitter.isPlaying)
				flameEmitter.Stop();
		}
		if (Input.GetKey("left"))
		{
			GetComponent<Rigidbody>().AddTorque(transform.up * -4f);
		}
		if (Input.GetKey("right"))
		{
			GetComponent<Rigidbody>().AddTorque(transform.up * 4f);
		}


    }

	public IEnumerator FileBullet()
	{
		while (true)
		{
			if (Input.GetKey("space"))
			{
				GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
				bullet.transform.position = transform.position + transform.forward * 1.5f;
				bullet.transform.rotation = transform.rotation;
				bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 1000f);
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
	public IEnumerator enableCollision()
	{
		yield return new WaitForSeconds(2.0f);
		detectCollision = true;
	}
	public IEnumerator CheckOffScreen()
	{
		while (true)
		{
			Vector3 gameViewWorldMin = Camera.main.ScreenToWorldPoint(new Vector3(-30f, -30f, 30f));
			Vector3 gameViewWorldMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 30f, Screen.height + 30f, 30f));
			Vector3 spaceshipPos = transform.position;

			if (spaceshipPos.x < gameViewWorldMin.x)
			{
				spaceshipPos.x = gameViewWorldMax.x;
			}
			if (spaceshipPos.x > gameViewWorldMax.x)
			{
				spaceshipPos.x = gameViewWorldMin.x;
			}
			if (spaceshipPos.z < gameViewWorldMin.z)
			{
				spaceshipPos.z = gameViewWorldMax.z;
			}
			if (spaceshipPos.z > gameViewWorldMax.z)
			{
				spaceshipPos.z = gameViewWorldMin.z;
			}
			transform.position = spaceshipPos;

			yield return new WaitForSeconds(0.2f);
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (detectCollision && ((c.gameObject.name == "Asteroid(Clone)") || (c.gameObject.name == "SmallAsteroid(Clone)")))
		{
			Destroy(gameObject);
			GameManager.instance.CreatePlayerShip();
		}
	}
}
