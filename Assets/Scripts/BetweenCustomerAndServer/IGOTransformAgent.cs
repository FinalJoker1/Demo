using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGOTransformAgent
{
    public List<Vector3> RecordPointInWorld { get; }
    public Vector3 DefaultPoint { get; }
    public Quaternion DefaultRotation { get; }
    public Vector3 DefaultScale { get; }

    public Transform DefaultParent { get; }

    public void SetParent(Transform parent,bool keepWordSpacePosition = false);

    public void MoveTo(int index);

    public void Reset();
}
