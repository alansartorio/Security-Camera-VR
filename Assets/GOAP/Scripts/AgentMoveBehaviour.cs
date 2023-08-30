using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Scripts
{
    public class AgentMoveBehaviour : MonoBehaviour
    {
        private AgentBehaviour agent;
        private NavMeshAgent navAgent;

        private void Awake()
        {
            agent = GetComponent<AgentBehaviour>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            agent.Events.OnTargetChanged += OnTargetChanged;
        }

        private void OnDisable()
        {
            agent.Events.OnTargetChanged -= OnTargetChanged;
        }

        private void OnTargetChanged(ITarget target, bool inRange)
        {
            navAgent.SetDestination(target.Position);
        }
    }
}