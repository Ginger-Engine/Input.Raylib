// RaylibPointerProvider.cs

using System.Numerics;
using Engine.Input.PointerEvents;

namespace Engine.Input.Raylib;

public class RaylibPointerProvider : IPointerProvider
{
    public Vector2 GetPointerPosition()
    {
        return Raylib_cs.Raylib.GetMousePosition();
    }
}