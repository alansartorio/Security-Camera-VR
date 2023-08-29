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
            agent.SetGoal<WanderGoal>(false);
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

            if ((client.leftItemsToGet > 0 || client.leftItemsToSteal > 0) && Random.Range(0, 2) == 1)
            {
                if (Random.Range(0, client.leftItemsToGet + client.leftItemsToSteal) < client.leftItemsToGet)
                {
                    agent.SetGoal<GetItemsGoal>(false);
                }
                else
                {
                    agent.SetGoal<StealItemsGoal>(false);
                }
                return;
            }

            if (client.leftItemsToGet == 0 && client.leftItemsToSteal == 0)
            {
                agent.SetGoal<ExitGoal>(false);
                return;
            }

            agent.SetGoal<WanderGoal>(false);
        }
    }
}