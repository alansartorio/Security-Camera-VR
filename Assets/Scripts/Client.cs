using System;
using CrashKonijn.Goap.Behaviours;
using GOAP.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Client : MonoBehaviour
{
    public int maxItemCapacity;
    public int leftItemsToGet;
    public int leftItemsToSteal;
    public int itemsInHand;
    public float thiefProbability = 0.1f;
    public int itemsHidden;
    public int itemsInCart;
    public int itemsInBag;
    private GameStateManager gameStateManager;

    private AgentBehaviour agent;

    private void Awake()
    {
        gameStateManager = GameObject.FindWithTag("GameManager").GetComponent<GameStateManager>();

        int totalItems = Random.Range(0, maxItemCapacity);
        bool steals = Random.Range(0f, 1) < thiefProbability;
        if (totalItems > 0 && steals)
        {
            leftItemsToSteal = Random.Range(1, totalItems + 1);
            leftItemsToGet = totalItems - leftItemsToSteal;
        }
        else
        {
            leftItemsToGet = totalItems;
            leftItemsToSteal = 0;
        }

        itemsInHand = 0;

        agent = GetComponent<AgentBehaviour>();
    }

    public void Exited()
    {
        if (itemsHidden > 0)
        {
            FindObjectOfType<PopupMonitor>().ShowPopup(PopupType.AThiefEscaped);

            gameStateManager.IncrementThiefEscapes();
            gameStateManager.Sanction();
        }

        Destroy(gameObject);
    }

    public void AccuseOfTheft()
    {
        if (itemsHidden > 0 || agent.CurrentAction is StealItemAction)
        {
            FindObjectOfType<PopupMonitor>().ShowPopup(PopupType.NiceCatch);

            gameStateManager.IncrementCatchedThieves();
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<PopupMonitor>().ShowPopup(PopupType.WrongAccusation);

            gameStateManager.IncrementWronglyAccused();
            gameStateManager.Sanction();
        }
    }
}