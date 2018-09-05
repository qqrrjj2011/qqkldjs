using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasAdptiveCom : MonoBehaviour {

    private CanvasScaler scaler;
    void Awake () {
        scaler = transform.GetComponent<CanvasScaler> ();

    }
    void Start () {
        ResetScaler ();
    }

    void ResetScaler () {
        if (scaler == null) {
            return;
        }
        ResolutionType type = DeviceUtil.ScreenType ();
        if (type == ResolutionType.Screen_Large) {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2 (1920, 1080); //(1334, 750);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            SetCamera (5f);
        } else if (type == ResolutionType.Screen_Small) {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2 (1136, 640);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0;
            SetCamera (5f);
        } else if (type == ResolutionType.Screen_Small_Ipad) {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2 (2048, 1536);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
            SetCamera (5.33f);
        } else if (type == ResolutionType.Screen_Large_Ipad) {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2 (2048, 1536);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
            SetCamera (5.33f);
        }

        //Debug.LogErrorFormat("-----ResetScaler 执行完---name = {0}", transform.gameObject.name);
    }

    void SetCamera (float value) {
        Camera.main.orthographicSize = value;
    }
}