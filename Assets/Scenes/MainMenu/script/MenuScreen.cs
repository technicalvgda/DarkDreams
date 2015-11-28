using UnityEngine;
using System.Collections;

public abstract class MenuScreen : MonoBehaviour
{
    public virtual void InitSettings() { }
    public abstract void Activate();
    public abstract void Deactivate();
    public virtual void SetAllInteractable(bool arg) { }
}
