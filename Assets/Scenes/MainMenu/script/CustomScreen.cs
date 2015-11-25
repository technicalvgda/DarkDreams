using UnityEngine;
using System.Collections;

public abstract class CustomScreen : MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void SetAllInteractable(bool arg);
}
