using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauricioAnimation : MonoBehaviour
{
    [SerializeField] private Animator _mauricioAnimator;
    [SerializeField] private Rigidbody2D _mauricioRigidbody;

    private void Update()
    {
        _mauricioAnimator.SetBool("MauriciOn", !Mathf.Approximately(_mauricioRigidbody.velocity.magnitude, 0f));
    }
}
