using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                _agent.destination = hit.point;
                SetAnimationState("Walking");
            }
        }

        if (_agent.remainingDistance == 0 && !_anim.GetBool("Idling") )
        {
            SetAnimationState("Idling");
        }
    }

    /// <summary>
    /// Sets all Animator parameters except one to false, which works for our purposes in this test.
    /// </summary>
    /// <param name="state">The state to set to true.</param>
    private void SetAnimationState(string state)
    {
        foreach(AnimatorControllerParameter p in _anim.parameters)
        {
            _anim.SetBool(p.name, p.name == state ? true : false);
        }
    }
}
