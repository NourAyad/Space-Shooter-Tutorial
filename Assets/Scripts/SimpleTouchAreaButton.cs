﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    
    private bool touched;
    private int pointerID;
    private bool canFire;

    void Awake()
    {
        touched = false;
        canFire = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Set our start point
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;
        }
    }

    

    public void OnPointerUp(PointerEventData data)
    {
        //Reset Everything
        if (data.pointerId == pointerID)
        {
            touched = false;
            canFire = false;
        }
    }

    public bool CanFire ()
    {
        return canFire;
    }
}
