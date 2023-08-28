using System;
using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace GOAP.Scripts
{
    public class AgentBrain : MonoBehaviour
    {
        private AgentBehaviour agent;

        private void Awake()
        {
            agent = GetComponent<AgentBehaviour>();
        }

        private void Start()
        {
            agent.SetGoal<WanderGoal>(false);
        }
    }
}