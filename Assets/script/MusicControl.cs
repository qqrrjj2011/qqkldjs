using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum soundType {
	fashe,
	huishou,
	pengzhuang,
	xiaohui,
	thunder,
	dogEat,
	fail,
	bgShengming,
	bgZuoteng
}
public class MusicControl : MonoBehaviour {

	public AudioClip fashe;
	public AudioClip huishou;
	public AudioClip xiaohui;
	public AudioClip pengzhuang;

	public AudioClip thunder;

	public AudioClip dogEat;

	public AudioClip fail;

	public AudioClip bgShengming;

	public AudioClip bgZuoteng;

	AudioSource audioSource;
	float pretTime = 0;
	List<float> soundTimes = new List<float> ();
	// Use this for initialization
	void Start () {
		GameMgr.inst ().musicControl = this;
		audioSource = GetComponent<AudioSource> ();
	}

	public void playSound (soundType type) {

		switch (type) {
			case soundType.fashe:
				audioSource.PlayOneShot (fashe);
				break;
			case soundType.huishou:
				audioSource.PlayOneShot (huishou);
				break;
			case soundType.pengzhuang:
				{
					
					//Debug.Log("time>>>>>>>>>>>>>>"+(Time.time - pretTime));
					//pretTime = Time.time;
					for (int i = soundTimes.Count - 1; i >= 0; i--) {
						if (Time.time - soundTimes[i] > 0.18f) {
							soundTimes.RemoveAt (i);
						}
					}

					if (soundTimes.Count < 4) {
						soundTimes.Add (Time.time);
						audioSource.PlayOneShot (pengzhuang);
					}
				}
				break;
			case soundType.xiaohui:
				audioSource.PlayOneShot (xiaohui);
				break;
			case soundType.dogEat:
				audioSource.PlayOneShot (dogEat);
				break;
			case soundType.thunder:
			{
				for (int i = soundTimes.Count - 1; i >= 0; i--) {
					if (Time.time - soundTimes[i] > 0.18f) {
						soundTimes.RemoveAt (i);
					}
				}

				if (soundTimes.Count < 4) {
					soundTimes.Add (Time.time);
					audioSource.PlayOneShot (thunder);
				}
				 
			}	
				break;
			case soundType.fail:
				audioSource.PlayOneShot(fail);
				break;
			default:
				break;
		}
	}


	public void playBgMusic(soundType type)
	{
		switch (type)
		{
			case soundType.bgShengming:
			audioSource.clip = bgShengming;
			audioSource.Play();
			break;
			case soundType.bgZuoteng:
			audioSource.clip = bgZuoteng;
			audioSource.Play();
			break;
			case soundType.fail:
			audioSource.clip = fail;
			audioSource.Play();
			break;
			default:
			break;
		}

	}

}