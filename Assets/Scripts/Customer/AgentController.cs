using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerAgentController : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;
    private CustomerAgent agent;

    public void Awake()
    {
        agent = GetComponent<CustomerAgent>();
    }

    public void Start()
    {
        Trigger();
    }

    public void Trigger()
    {
        if(isOpen)
        {
            agent.DisableAgent();
        }
        else
        {
            agent.EnableAgent();
        }
    }
}
