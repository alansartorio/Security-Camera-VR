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
        private ITarget currentTarget;
        private bool shouldMove;

        private void Awake()
        {
            agent = GetComponent<AgentBehaviour>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            agent.Events.OnTargetInRange += OnTargetInRange;
            agent.Events.OnTargetChanged += OnTargetChanged;
            agent.Events.OnTargetOutOfRange += OnTargetOutOfRange;
        }

        private void OnDisable()
        {
            agent.Events.OnTargetInRange -= OnTargetInRange;
            agent.Events.OnTargetChanged -= OnTargetChanged;
            agent.Events.OnTargetOutOfRange -= OnTargetOutOfRange;
        }

        private void OnTargetInRange(ITarget target)
        {
            shouldMove = false;
        }

        private void OnTargetChanged(ITarget target, bool inRange)
        {
            navAgent.SetDestination(target.Position);
            currentTarget = target;
            shouldMove = !inRange;
        }

        private void OnTargetOutOfRange(ITarget target)
        {
            shouldMove = true;
        }
    }
}