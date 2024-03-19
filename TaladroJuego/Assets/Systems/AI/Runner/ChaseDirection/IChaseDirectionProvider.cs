using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Runner.ChaseDirection
{
    public interface IChaseDirectionProvider
    {
        Vector2 DirectionToChase();
    }
}

