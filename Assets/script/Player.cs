using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Entity {
    bool indrag = false;
    public float eatScaleX = 0.8f;
    public float eatedScaleX = 1.2f;
    public float addScale = 0.35f;
    public float smallSpeed = 0.5f;

    public float dieScale = 0.2f;

    public float smalTimeGap = 3;

    public float maxScale = 10;

    float curSmalTime = 3;
    Vector3 oripos;

    bool gameOver = false;

    Vector3 oriScale;
    // Use this for initialization
    void Start () {
        oriScale = transform.localScale;
        oripos = transform.localPosition;
        GameMgr.inst().player = this;
        Debug.LogWarning("W:" + transform.GetComponent<SpriteRenderer>().bounds.size.x);
       
	}

    public void reStart()
    {
        transform.localScale = oriScale;
        transform.localPosition = oripos;
        DOTween.Sequence().AppendInterval(0.2f).AppendCallback(()=> {
            transform.localScale = oriScale;
            gameOver = false;
        });
        curSmalTime = smalTimeGap;
       
    }

   
	// Update is called once per frame
	void Update () {
        

        //if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        //{
        //    if (UIcontroller.getInst().IsPointerOverUIObject(Input.mousePosition) && !indrag)
        //    {
        //        //	Debug.Log(">>>>>>>>>>>>>>>>>click ui3");
        //        return;
        //    }
        //}



        if (GameMgr.inst().canAddSpeed && Input.GetMouseButtonUp(0))
        {
           
        }

        if (gameOver || GameMgr.inst().GetGameState() != gameState.ballRuning)
            return;


        if (Input.GetMouseButton(0))
        {
            indrag = true;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //	Debug.Log(">>>>>>>>>>>>>>GetMouseButton(0)");
            if (mousePos.x < lelfX)
                mousePos.x = lelfX;
            if (mousePos.x > rightX)
                mousePos.x = rightX;
            if (mousePos.y < downY)
                mousePos.y = downY;
            if (mousePos.y > upY)
                mousePos.y = upY;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            indrag = false;

        }
        curSmalTime -= Time.deltaTime;
        if (curSmalTime <= 0)
        {
            curSmalTime = smalTimeGap;
            Vector3 toScale = transform.localScale * smallSpeed;
            transform.DOScale(toScale, 0.3f).SetEase(Ease.InOutBack);
            if (transform.localScale.x < dieScale)
            {
                Time.timeScale = 0;
                
                UIcontroller.getInst().dieType = 2;
                gameOver = true;
                UIcontroller.getInst().gameOver(1);
            }
        }
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.localScale.x < transform.localScale.x * eatScaleX)
        {
            Vector3 toScale = collision.transform.localScale * addScale;
            if (transform.localScale.x + toScale.x > maxScale)
            {
                transform.localScale = new Vector3(maxScale, maxScale, 1);
            }
            else
            {
                transform.DOScale(transform.localScale + toScale, 0.3f).SetEase(Ease.OutBack);
            }

            GameMgr.inst().musicControl.playSound(soundType.xiaohui);

            curSmalTime += 0.8f;
            Destroy(collision.gameObject);
        }
        else if (collision.transform.localScale.x > transform.localScale.x * eatedScaleX)
        {
            Time.timeScale = 0;
            gameOver = true;
            UIcontroller.getInst().dieType = 1;
            UIcontroller.getInst().gameOver(1);
          //  Destroy(gameObject);
        }
       
    }
}
