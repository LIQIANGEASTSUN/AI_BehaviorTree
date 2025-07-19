using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphicTree
{

    public enum ParameterType
    {
        /// <summary>
        /// Float
        /// </summary>
        [EnumAttirbute("Float")]
        Float = 0,

        /// <summary>
        /// Int
        /// </summary>
        [EnumAttirbute("Int")]
        Int = 2,

        /// <summary>
        /// Long
        /// </summary>
        [EnumAttirbute("Long")]
        Long = 3,

        /// <summary>
        /// Bool
        /// </summary>
        [EnumAttirbute("Bool")]
        Bool = 5,

        /// <summary>
        /// String
        /// </summary>
        [EnumAttirbute("String")]
        String = 10,

    }


}
