using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTransformAgent : MonoBehaviour, IGOTransformAgent
{
    private List<Vector3> _pointList = new List<Vector3>();
    public List<Vector3> RecordPointInWorld => _pointList;

    private Vector3 _defaultPoint;
    public Vector3 DefaultPoint => _defaultPoint;

    public Quaternion DefaultRotation => _defaultQuaternion;

    public Vector3 DefaultScale => _defaultScale;

    private Quaternion _defaultQuaternion;
    private Vector3 _defaultScale;

    private Transform _defalutParent;

    public Transform DefaultParent => _defalutParent;

    public void Awake()
    {
        _defaultPoint = transform.position;
        _defaultQuaternion = transform.rotation;
        _defaultScale = transform.localScale;
        _defalutParent = transform.parent;

    }

    public void MoveTo(int index)
    {
        if(index < 0 || index + 1 > _pointList.Count)
            transform.position = _defaultPoint;
        else
            transform.position = _pointList[index];
    }

    public void SetParent(Transform parent, bool keepWordSpacePosition = false)
    {
        transform.SetParent(parent, keepWordSpacePosition);
        transform.localPosition = Vector3.zero;
    }

    public void Reset()
    {
        SetParent(DefaultParent);
        transform.position = DefaultPoint;
        transform.rotation = DefaultRotation;
        transform.localScale = DefaultScale;
    }
}
