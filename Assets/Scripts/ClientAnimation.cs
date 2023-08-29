using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using GOAP.Scripts;
using UnityEngine;

public class ClientAnimation : MonoBehaviour
{
    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Grab = Animator.StringToHash("grab");
    private static readonly int Gather = Animator.StringToHash("gather");
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject applePrefab;
    private Animator animator;
    private AgentBehaviour agent;
    private GameObject appleInHand;
    private static readonly int Steal = Animator.StringToHash("steal");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<AgentBehaviour>();
    }

    private void OnEnable()
    {
        agent.Events.OnTargetOutOfRange += OnTargetOutOfRange;
        agent.Events.OnTargetInRange += OnTargetInRange;
        agent.Events.OnActionStart += OnActionStart;
        agent.Events.OnActionStop += OnActionStop;
    }

    private void OnDisable()
    {
        agent.Events.OnTargetOutOfRange -= OnTargetOutOfRange;
        agent.Events.OnTargetInRange -= OnTargetInRange;
        agent.Events.OnActionStart -= OnActionStart;
        agent.Events.OnActionStop -= OnActionStop;
    }

    void OnTargetInRange(ITarget target)
    {
        animator.SetBool(Walking, false);
        if (agent.CurrentAction is GrabItemAction)
        {
            animator.SetTrigger(Grab);
            appleInHand = Instantiate(applePrefab, hand);
            appleInHand.transform.localScale = Vector3.one * 0.01f;
        }
    }

    void OnActionStart(IActionBase action)
    {
        if (action is GatherItemAction)
        {
            animator.SetTrigger(Gather);
        }
        else if (action is StealItemAction)
        {
            animator.SetTrigger(Steal);
        }
    }

    private void OnActionStop(IActionBase action)
    {
        if (action is GatherItemAction or StealItemAction)
        {
            Destroy(appleInHand);
        }
    }

    void OnTargetOutOfRange(ITarget target)
    {
        animator.SetBool(Walking, true);
    }
}