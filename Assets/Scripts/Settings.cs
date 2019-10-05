using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    /// <summary>
    /// whether or not the game will run in full 6dof controlls or top-down
    /// </summary>
    [Tooltip("whether or not the game will run in full 6dof controlls or top-down")]
    public bool m_is_3d = false;
}
