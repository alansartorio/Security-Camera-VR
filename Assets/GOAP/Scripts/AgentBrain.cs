using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GOAP.Scripts
{
    public class AgentBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private Client client;

        private void Awake()
        {
            agent = GetComponent<AgentBehaviour>();
            client = GetComponent<Client>();
        }

        private void Start()
        {
            agent.SetGoal<WanderGoal>(true);
        }

        private void OnEnable()
        {
            agent.Events.OnActionStop += OnActionStop;
        }

        private void OnDisable()
        {
            agent.Events.OnActionStop -= OnActionStop;
        }

        private void OnActionStop(IActionBase action)
        {
            if (client.itemsInHand > 0) return;
            
            if (client.leftItemsToGrab > 0 && Random.Range(0, 2) == 1)
            {
                agent.SetGoal<ObtainItemsGoal>(false);
                return;
            }
            
            agent.SetGoal<WanderGoal>(false);
        }
    }
}