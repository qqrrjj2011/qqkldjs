using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAtion : MonoBehaviour {
	public bool awakePlay = false;
	// Use this for initialization
	public Vector3 offsetScale = new Vector3(1.0f,1.0f,0);

	public float duration = 0.5f;
	float curTime = 0.0f;
	Vector3 oriScale = new Vector3(0,0,0);
	Vector3 targetScale;

	Vector3 speedScale = new Vector3(0,0,0);

	Vector3 curSpeedScale = new Vector3(0,0,0);

	bool playing = false;
	void Awake()
	{
		oriScale = transform.localScale;

		targetScale = oriScale+offsetScale;

		speedScale = offsetScale / duration;

		if(awakePlay)
			playing = awakePlay;
	}
	void Start () {
		curSpeedScale = speedScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing)
		{
		 
			transform.localScale += curSpeedScale * Time.deltaTime;
			if(Mathf.Abs(transform.localScale.x - oriScale.x) > Mathf.Abs(offsetScale.x))
			{
				if(transform.localScale.x > oriScale.x)
					curSpeedScale = speedScale * -1.0f;
				else
				{
					curSpeedScale = speedScale;
				}
			}
		}
	}

	public void resetPlay()
	{
		transform.localScale = oriScale;
		playing = true;
	} 

	public void resetStop()
	{
		transform.localScale = oriScale;
		playing = false;
	} 

	public void pause()
	{
		playing = false;
	}

	public void resume()
	{
		playing = true;
	}

	public void play()
	{
		playing = true;
	}


}
