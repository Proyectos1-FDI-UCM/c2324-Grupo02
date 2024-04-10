using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauricioAnimation : MonoBehaviour
{
    [SerializeField] private Animator _mauricioAnimator;
    [SerializeField] private Rigidbody _mauricioRigidbody;

    private void FixedUpdate()
    {
        if (_mauricioRigidbody.velocity != Vector3.zero)
        {
            _mauricioAnimator.SetInteger("moving", 1);
        }
        else
        {
            _mauricioAnimator.SetInteger("moving",0); 
        }
    }
}
