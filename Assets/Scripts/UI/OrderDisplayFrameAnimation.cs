using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDisplayFrameAnimation : MonoBehaviour
{
    private Animation _animation;
    public List<string> animationsName = new List<string>();

    public void Start()
    {
        //Icon在Awake加载，如果要使用Icon，须在Awake之后一段时间
        _animation = GetComponent<OrderDisplayFrameMono>()?.Icon?.GetComponent<Animation>();
        _animation?.Play(animationsName[0]);
    }
}
