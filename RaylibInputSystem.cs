using System.Numerics;
using Engine.Input.Abstractions;
using Raylib_cs;

namespace Engine.Input.RaylibInput;

public class RaylibInputSystem : IInputSystem
{
    private readonly Dictionary<string, IInputAction> _actions = new();

    public RaylibInputSystem(Dictionary<string, IInputAction> actions)
    {
        _actions = actions;
    }

    public IInputAction<T>? Get<T>(string actionId)
    {
        return _actions.TryGetValue(actionId, out var action) ? (IInputAction<T>)action : null;
    }

    public void Update()
    {
        foreach (var pair in _actions)
        {
            switch (pair.Value)
            {
                case InputAction<bool> button:
                    bool isDown = Raylib.IsKeyDown(Enum.Parse<KeyboardKey>(pair.Key));
                    button.Update(isDown);
                    break;
                case InputAction<float> axis:
                    float axisValue = Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftY);
                    axis.Update(axisValue);
                    break;
                case InputAction<Vector2> vec2:
                    Vector2 v = new(Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftX), Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftY));
                    vec2.Update(v);
                    break;
            }
        }
    }
}
