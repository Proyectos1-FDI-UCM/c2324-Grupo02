using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusSystem
{
    public interface IStatusParameter
    {
        void AugmentValue(float value);

        void ReduceValue(float value);
    }
}

