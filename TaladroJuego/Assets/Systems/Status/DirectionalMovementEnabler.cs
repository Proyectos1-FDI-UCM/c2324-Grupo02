using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;

public class DirectionalMovementEnabler : MonoBehaviour
{
    [SerializeField] private StatusParameter _statusParameter;
    [SerializeField] private Rigidbody2D _shipBody;
    [SerializeField] private GameObject _shipDirectional;

    void Start()
    {
        
    }

    void Update()
    {
        if(_statusParameter.Value <= 0)
        {
            if(_shipBody.velocity == Vector2.zero)
                _shipDirectional.SetActive(false);
        }
        else
        {
            _shipDirectional.SetActive(true);
        }
    }
}
