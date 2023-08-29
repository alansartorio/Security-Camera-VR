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
    private static readonly int Steal = Animator.StringToHash("steal");
    private static readonly int Pay = Animator.StringToHash("pay");
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject applePrefab;
    private Animator animator;
    private AgentBehaviour agent;
    private GameObject appleInHand;

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
        if (agent.CurrentAction is GrabItemAction && appleInHand == null)
        {
            animator.SetTrigger(Grab);
            appleInHand = Instantiate(applePrefab, hand);
            appleInHand.transform.localScale = Vector3.one * 0.01f;
        } else if (agent.CurrentAction is PayAction)
        {
            animator.SetTrigger(Pay);
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
            appleInHand = null;
        }
    }

    void OnTargetOutOfRange(ITarget target)
    {
        animator.SetBool(Walking, true);
    }
}