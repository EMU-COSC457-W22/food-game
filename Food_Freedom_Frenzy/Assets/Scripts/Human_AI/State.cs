using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void EnterState(HumanManager human);
    public abstract void UpdateState(HumanManager human);
}
