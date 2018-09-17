using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public float speed = 5;
    public float scale = 1;

    public float topY = 5;
    public float bottomY = -5;
    public float lelfX = -2.8f;
    public float rightX = 2.8f;

    public float upY = 4.6f;
    public float downY = -4.6f;



    protected bool move = false;

    protected float speedDt; 



 
}
