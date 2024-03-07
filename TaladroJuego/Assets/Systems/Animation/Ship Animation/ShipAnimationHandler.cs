using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationSystem
{
    internal class ShipAnimationHandler : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Rigidbody2D _shipRigidBody;

        void FixedUpdate()
        {
            _animator.SetBool("Moving", !Mathf.Approximately(_shipRigidBody.velocity.magnitude, 0));
        }
    }
}

