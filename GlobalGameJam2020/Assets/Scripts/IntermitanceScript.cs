using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermitanceScript : MonoBehaviour
{
	public int beginTime = 0;
	public int onTime = 10;
	public int offTime = 0;

	private Hazard hazard;
	private float timer;
	// Start is called before the first frame update
	void Start()
	{
		hazard = GetComponent<Hazard>();
		hazard.on = false;
		timer = beginTime;
	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0.0f)
		{
			Switch();
		}
	}

	void Switch() {
		if(hazard.on) {
			timer = offTime;
		} else {
			timer = onTime;
		}

		hazard.on = !hazard.on;
	}
}
