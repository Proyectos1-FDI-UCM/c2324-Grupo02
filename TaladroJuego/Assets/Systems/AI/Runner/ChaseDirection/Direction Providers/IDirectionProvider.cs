using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Runner.ChaseDirection
{
    public interface IDirectionProvider
    {
        Vector2 DirectionToChase();
    }
}

