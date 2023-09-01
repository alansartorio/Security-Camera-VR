using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using GOAP.Scripts;
using UnityEngine;

public class ClientSounds : MonoBehaviour
{
    private AgentBehaviour agent;
    private AudioSource footstep;

    private void Awake()
    {
        agent = GetComponent<AgentBehaviour>();
        footstep = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        agent.Events.OnTargetOutOfRange += OnTargetOutOfRange;
        agent.Events.OnTargetInRange += OnTargetInRange;
    }

    private void OnDisable()
    {
        agent.Events.OnTargetOutOfRange -= OnTargetOutOfRange;
        agent.Events.OnTargetInRange -= OnTargetInRange;
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