using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    public Transform enemyPrefab;
    public float startY = 6;
    public float startX = -3;
    public float endY = -6;
    public float gap = 1.2f;
    public float minScale = 0.1f;
    public float maxScale = 1;
    public int col = 5;

    public float newRowTime = 0.6f;

    public Transform player;

    public float sizeX = 1;
 

    SpriteRenderer spriteRenderer;
    static EnemyControl instance;
    public static EnemyControl getInst()
    {
        return instance;
    }
    // Use this for initialization
    void Start () {
        instance = this;
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        sizeX = spriteRenderer.bounds.size.x;
        
	}

    public void myStart()
    {
        StartCoroutine(createEnemy());

    }

    public void reStart()
    {
        clear();
        StartCoroutine(createEnemy());
    }

    IEnumerator createEnemy()
    {
        while (true)
        {

            // gap = (sizeX + 0.2f);


            for (int i = 0; i < col; i++)
            {
                Transform eny = Transform.Instantiate(enemyPrefab);
                eny.SetParent(transform);
                eny.localPosition = new Vector3(-3 + gap * i + Random.Range(-1f, 1.0f), startY + Random.Range(-1f, 1f), 0);
                Vector3 toScale = new Vector3(1,1,1) * Random.Range(minScale, maxScale);
                if (toScale.x > maxScale)
                {
                    toScale = new Vector3(maxScale, maxScale, 1);
                }
                eny.localScale = toScale;
            }
            yield return new WaitForSeconds(newRowTime);
           
        }
    }

    public void clear()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        StopAllCoroutines();

    }
    
}
