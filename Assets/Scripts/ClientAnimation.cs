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
    private Animator animator;
    private AgentBehaviour agent;

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
    }

    private void OnDisable()
    {
        agent.Events.OnTargetOutOfRange -= OnTargetOutOfRange;
        agent.Events.OnTargetInRange -= OnTargetInRange;
        agent.Events.OnActionStart -= OnActionStart;
    }

    void OnTargetInRange(ITarget target)
    {
        animator.SetBool(Walking, false);
        if (agent.CurrentAction is GrabItemAction)
        {
            animator.SetTrigger(Grab);
        }
    }

    void OnActionStart(IActionBase action)
    {
        if (action is GatherItemAction)
        {
            animator.SetTrigger(Gather);
        }
    }
    
    void OnTargetOutOfRange(ITarget target)
    {
        animator.SetBool(Walking, true);
    }

}
