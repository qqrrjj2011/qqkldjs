using System.Collections;
using UnityEngine;

public class DeviceUtil {
    public static ResolutionType ScreenType () {
        ResolutionType type = ResolutionType.Screen_Notscale;
        //#if UNITY_IPHONE || UNITY_ANDROID
        //        float w = Screen.currentResolution.width;
        //        float h = Screen.currentResolution.height;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        float w = Screen.width;
        float h = Screen.height;
#else
        float w = Screen.currentResolution.width;
        float h = Screen.currentResolution.height;
#endif
        float rate = h / w;
        if (rate < 1.34) //以ipad为标准
        {
            if (h > 1024) {
                type = ResolutionType.Screen_Large_Ipad;
            } else if (h <= 1024) {
                type = ResolutionType.Screen_Small_Ipad;
            }
        } else //剩下为iphone
        {
            if (h > 1334) {
                type = ResolutionType.Screen_Large;
            } else if (h <= 1334) {
                type = ResolutionType.Screen_Small;
            }
        }

        return type;
    }

    /// <summary>
    /// UI的适配
    /// </summary>
    /// <returns></returns>
    public static bool UIResolutionType1136X640 () {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        float w = Screen.width;
        float h = Screen.height;
#else
        float w = Screen.currentResolution.width;
        float h = Screen.currentResolution.height;
#endif
        if (w % 1136 == 0 && h % 640 == 0) {
            return true;
        }
        if (w % 1024 == 0 && h % 768 == 0) {
            return false;
        }
        ResolutionType type = ScreenType ();
        if (type == ResolutionType.Screen_Large || type == ResolutionType.Screen_Small) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// UGUI适配是否用了768
    /// </summary>
    /// <returns></returns>
    public static bool UIResolutionType768 () {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        float w = Screen.width;
        float h = Screen.height;
#else
        float w = Screen.currentResolution.width;
        float h = Screen.currentResolution.height;
#endif
        ResolutionType type = ScreenType ();
        if (type == ResolutionType.Screen_Large_Ipad || type == ResolutionType.Screen_Small_Ipad) {
            return true;
        } else if (type == ResolutionType.Screen_Notscale && h >= 768) {
            return true;
        }
        return false;
    }
}

public enum ResolutionType {
    /// <summary>
    /// 不缩放  以1136X640
    /// </summary>
    Screen_Notscale,
    /// <summary>
    /// 放大
    /// </summary>
    Screen_Large,
    /// <summary>
    /// 缩小
    /// </summary>
    Screen_Small,
    /// <summary>
    /// 以Ipad为标准缩放 1024X768
    /// </summary>
    Screen_Large_Ipad,
    Screen_Small_Ipad,
}