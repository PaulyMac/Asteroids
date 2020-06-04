using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Vector3 gameViewWorldMin;
	private Vector3 gameViewWorldMax;
    // Start is called before the first frame update
    void Start()
    {
		gameViewWorldMin = Camera.main.ScreenToWorldPoint(new Vector3(-30f, -30f, 30f));
		gameViewWorldMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 30f, Screen.height + 30f, 30f));
		StartCoroutine(CheckOffscreen());
    }

    // Update is called once per frame
    void Update()
    {

    }

	public IEnumerator CheckOffscreen()
	{
		while (true)
		{
			if ((transform.position.x < gameViewWorldMin.x) || (transform.position.z < gameViewWorldMin.z) || (transform.position.x > gameViewWorldMax.x) || (transform.position.z > gameViewWorldMax.z))
			{
				Destroy(gameObject);
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
}
