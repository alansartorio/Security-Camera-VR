using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using GOAP.Scripts;
using UnityEngine;

public class ClientSounds : MonoBehaviour
{
    private AgentBehaviour agent;
    private AudioSource footstep;
    private AudioSource cashCounter;

    private void Awake()
    {
        agent = GetComponent<AgentBehaviour>();
        footstep = GetComponent<AudioSource>();
        cashCounter = GameObject.FindWithTag("Counter").GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        agent.Events.OnTargetOutOfRange += OnTargetOutOfRange;
        agent.Events.OnTargetInRange += OnTargetInRange;
        agent.Events.OnActionStop += OnActionStop;
    }

    private void OnDisable()
    {
        agent.Events.OnTargetOutOfRange -= OnTargetOutOfRange;
        agent.Events.OnTargetInRange -= OnTargetInRange;
        agent.Events.OnActionStop -= OnActionStop;
    }

    private void OnActionStop(IActionBase action)
    {
        if (action is PayAction)
        {
            cashCounter.Stop();
            cashCounter.Play();
        }
    }

    void OnTargetInRange(ITarget target)
    {
        footstep.enabled = false;
    }

    void OnTargetOutOfRange(ITarget target)
    {
        footstep.enabled = true;
    }
}