using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Vector3 randomDir = Random.onUnitSphere;
		randomDir.y = 0f;
        GetComponent<Rigidbody>().AddForce(randomDir * 500f);
		GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 500f);

		StartCoroutine(CheckOffScreen());
    }

    // Update is called once per frame
    void Update()
    {

    }
	public IEnumerator CheckOffScreen()
	{
		while (true)
		{
			Vector3 gameViewWorldMin = Camera.main.ScreenToWorldPoint(new Vector3(-30f, -30f, 30f));
			Vector3 gameViewWorldMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 30f, Screen.height + 30f, 30f));
			Vector3 asteroidPos = transform.position;

			if (asteroidPos.x < gameViewWorldMin.x)
			{
				asteroidPos.x = gameViewWorldMax.x;
			}
			if (asteroidPos.x > gameViewWorldMax.x)
			{
				asteroidPos.x = gameViewWorldMin.x;
			}
			if (asteroidPos.z < gameViewWorldMin.z)
			{
				asteroidPos.z = gameViewWorldMax.z;
			}
			if (asteroidPos.z > gameViewWorldMax.z)
			{
				asteroidPos.z = gameViewWorldMin.z;
			}
			transform.position = asteroidPos;

			yield return new WaitForSeconds(0.2f);
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.name == "Bullet(Clone)")
		{
			Destroy(gameObject);
			Destroy(c.gameObject);
      GameManager.instance.IncrementScore(10);

			if (gameObject.name == "Asteroid(Clone)")
			{
				Destroy(gameObject);
				for (int i = 0; i < 3; i++)
				{
					GameObject smallasteroid = (GameObject)Instantiate(Resources.Load("SmallAsteroid"));
					smallasteroid.transform.position = transform.position;
				}
			}

		}
	}

}
