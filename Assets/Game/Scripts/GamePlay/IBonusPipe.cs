
using System;
using UnityEngine;

public interface IBonusPipeView
{
    event Action OnClicked;
    Vector3 Position { get; }
    Quaternion Rotation { get; }
    void DestroyPipe();
}
