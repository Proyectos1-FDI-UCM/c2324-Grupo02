using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusSystem
{
    /// <summary>
    /// Represents a status parameter with a float value.
    /// </summary>
    public interface IStatusParameter
    { 
        /// <summary>
        /// Gets or sets the value of the status parameter.
        /// </summary>
        float Value { get; set; }
    }
}

