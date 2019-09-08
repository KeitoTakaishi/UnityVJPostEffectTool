using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
using Random = System.Random;


public class CameraAnimation : MonoBehaviour
{
    #region Nested Classes
    public enum Interpolator
    {
        Lerp,
        Slerp,
        Expo
    }
    #endregion

    #region Editable Properties
    [SerializeField] Interpolator _interpolator;
    [SerializeField]  private Transform _target;
    [SerializeField] private int _interval = 60;
    [SerializeField] private AnimationCurve _anim;
    [SerializeField] private float _radius = 15.0f;
    [SerializeField] private bool isInterpolation = true;

    public Interpolator interpolator
    {
        get { return _interpolator; }
        set { _interpolator = value; }
    }

    public Transform target
    {
        get { return _target; }
        set { _target = value; }
    }

    public int Interval
    {
        get { return _interval; }
        set { _interval = value; }
    }

    public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

    public bool IsInterpolation
    {
        get { return isInterpolation; }
        set { isInterpolation = value; }
    }
    #endregion


    #region Private Properties
    Vector3 _nextPos, _curPos;
    private float t = 0.0f;
    #endregion

    void Start()
    {
        _nextPos = new Vector3();
        _curPos = new Vector3();
    }

    private void Update()
    {
        if (isInterpolation)
        {
            Interpolation();
        }
        else
        {
            Pan();
        }
    }

    #region Interpolation Func

    public bool IsInterpolation1
    {
        get { return isInterpolation; }
        set { isInterpolation = value; }
    }

    void Interpolation()
    {
        var _dt = 1.0f / _interval;
        if (isInterval())
        {
            _nextPos = NextPos();
            _curPos = this.transform.position;
            t = 0.0f;
        }

        LookAt();

        if (_interpolator == Interpolator.Lerp)
        {
            this.transform.position = Vector3.Lerp(_curPos, _nextPos, t);
        }
        else if (_interpolator == Interpolator.Expo)
        {
            var val = _anim.Evaluate(t);
            this.transform.position = Interpolation(_curPos, _nextPos, val);
        }
        t += _dt;
    }

    private Vector3 NextPos()
    {

        var _nextPos = UnityEngine.Random.insideUnitSphere * _radius + target.transform.position;
        return _nextPos;
    }

    protected virtual void LookAt()
    {
        //毎フレームTargetの方向を向くようにする
        if (target == null) return;
        this.gameObject.transform.LookAt(target.transform.position);
    }

    bool isInterval()
    {
        return Time.frameCount % _interval == 1;
    }

    private Vector3 Interpolation(Vector3 curPos, Vector3 nextPos, float t)
    {
        Debug.Log(t);
        Vector3 pos = (nextPos - curPos);
        pos = pos * t + curPos;
        return pos;
    }

    #endregion

    void Dolly()
    {
        this.transform.LookAt(target.transform.position);
        this.transform.SetParent(target.transform);
    }

    void Pan()
    {
        this.transform.LookAt(target.transform.position);
    }

    float h = 0.0f;
    void Roll()
    {
        this.transform.LookAt(target.transform.position);
        var dir = (target.transform.position - this.transform.position).normalized;

        h += Input.GetAxis("Horizontal");
        this.transform.Rotate(Vector3.forward, h);
    }

    void Zoom()
    {
        transform.LookAt(target.transform.position);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Camera.main.fieldOfView < 170.0f)
            {
                Camera.main.fieldOfView += 0.5f;
            }


        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Camera.main.fieldOfView > 2.0f)
            {
                Camera.main.fieldOfView -= 0.5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Camera.main.orthographic = !Camera.main.orthographic;
        }
    }
}