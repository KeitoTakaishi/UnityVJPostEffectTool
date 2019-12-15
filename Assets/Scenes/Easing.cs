using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing : MonoBehaviour
{
    static public float easeInQuad(float t)  
    {
        return t * t;
    }

    static public float easeOutQuad(float t)
    {
        return -1.0f * t * (t - 2.0f);
    }

    static public float easeInOutQuad(float t)
    {
        if((t *= 2.0f) < 1.0)
        {
            return 0.5f * t * t;
        } else
        {
            return -0.5f * ((t - 1.0f) * (t - 3.0f) - 1.0f);
        }
    }

    static public float easeInCubic(float t)
    {
        return t * t * t;
    }

    static public float easeOutCubic(float t)
    {
        return (t = t - 1.0f) * t * t + 1.0f;
    }

    static public float easeInOutCubic(float t)
    {
        if((t *= 2.0f) < 1.0f)
        {
            return 0.5f * t * t * t;
        } else
        {
            return 0.5f * ((t -= 2.0f) * t * t + 2.0f);
        }
    }

    static public float easeInExpo(float t)
    {
        return (t == 0.0f) ? 0.0f : Mathf.Pow(2.0f, 10.0f * (t - 1.0f));
    }

    static public float easeOutExpo(float t)
    {
        return (t == 1.0f) ? 1.0f : -Mathf.Pow(2.0f, -10.0f * t) + 1.0f;
    }

    static public float easeInOutExpo(float t)
    {
        if(t == 0.0 || t == 1.0)
        {
            return t;
        }
        if((t *= 2.0f) < 1.0f)
        {
            return 0.5f * Mathf.Pow(2.0f, 10.0f * (t - 1.0f));
        } else
        {
            return 0.5f * (-Mathf.Pow(2.0f, -10.0f * (t - 1.0f)) + 2.0f);
        }
    }

}
