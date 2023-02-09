using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void Awake()
    {
        // 해상도 대응을 위한 코드
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float ratio = (float)Screen.width / Screen.height;
        float scaleWidht = ratio / ((float)16 / 9);
        float scaleHeight = 1 / scaleWidht;
        if (scaleWidht < 1)
        {
            rect.height = scaleWidht;
            rect.y = (1 - scaleWidht) / 2;
        }
        else
        {
            rect.width = scaleHeight;
            rect.x = (1 - scaleHeight) / 2;
        }
        camera.rect = rect;
    }
}
