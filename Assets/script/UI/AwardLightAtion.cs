using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AwardLightAtion : MonoBehaviour {

	// Use this for initialization
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	Transform bg;
	Transform icon;
	Transform txt;

	public float speed = 15.0f;

	float runTime = 0;
	void Awake()
	{
		bg = transform.Find("bg");
		icon = transform.Find("icon");
		txt = transform.Find("text");
	}
 


	public void init(int num, string iconName = "")
	{
		txt.GetComponent<Text>().text = "x "+num;
		 
		StartCoroutine(bgRotate());
	}

	IEnumerator bgRotate()
	{	
		icon.localScale = new Vector2(0.3f,0.3f);
		yield return new WaitForSecondsRealtime(0.3f);
		
		icon.DOScale(new Vector2(1,1),0.7f).SetEase(Ease.OutBounce).SetUpdate(true);

		while (runTime < 10)
		{
			runTime += Time.unscaledDeltaTime;
			bg.Rotate(new Vector3(0,0,1),speed * Time.unscaledDeltaTime);

			if(Input.GetMouseButton(0))
			{
				Destroy(gameObject);
			}
			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
		 
	}

	
 
}
