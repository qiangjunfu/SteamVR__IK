using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    public abstract int GetId();
    public virtual bool IsDead() { return false; }
}
