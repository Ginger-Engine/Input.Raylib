using System;
using System.Collections.Generic;
using System.Numerics;
using Engine.Input.Devices;
using Raylib_cs;

namespace Engine.Input.Raylib.Devices;

public class MouseDevice : IInputDevice
{
    public string Id => "Mouse";

    public Dictionary<string, InputValueType> Keys => new()
    {
        ["Left"]   = InputValueType.Bool,
        ["Right"]  = InputValueType.Bool,
        ["Middle"] = InputValueType.Bool,
        ["Wheel"]  = InputValueType.Float,
        ["Delta"]  = InputValueType.Vector2,
        ["DeltaX"] = InputValueType.Float,
        ["DeltaY"] = InputValueType.Float,
    };

    private readonly Dictionary<string, MouseButton> _buttonMap = new()
    {
        ["Left"]   = MouseButton.Left,
        ["Right"]  = MouseButton.Right,
        ["Middle"] = MouseButton.Middle,
    };

    private readonly Dictionary<string, bool> _prevState = new();
    private readonly Dictionary<string, bool> _currState = new();

    public event Action<string>? OnPressed;
    public event Action<string>? OnReleased;

    private Vector2 _lastMousePosition;
    private Vector2 _delta;
    private float _wheel;

    public void Update()
    {
        // Обновление позиции и дельты
        var currentPos = new Vector2(Raylib_cs.Raylib.GetMouseX(), Raylib_cs.Raylib.GetMouseY());
        _delta = currentPos - _lastMousePosition;
        _lastMousePosition = currentPos;

        _wheel = Raylib_cs.Raylib.GetMouseWheelMove();

        // Обработка событий кнопок
        foreach (var (name, button) in _buttonMap)
        {
            bool isDown = Raylib_cs.Raylib.IsMouseButtonDown(button);
            _currState[name] = isDown;

            bool wasDown = _prevState.TryGetValue(name, out var prev) && prev;

            if (isDown && !wasDown)
                OnPressed?.Invoke(name);
            else if (!isDown && wasDown)
                OnReleased?.Invoke(name);

            _prevState[name] = isDown;
        }
    }

    public object? ReadValue(string key)
    {
        return key switch
        {
            "Left"   => _currState.TryGetValue("Left", out var l) ? l : false,
            "Right"  => _currState.TryGetValue("Right", out var r) ? r : false,
            "Middle" => _currState.TryGetValue("Middle", out var m) ? m : false,
            "Wheel"  => _wheel,
            "Delta"  => _delta,
            "DeltaX" => _delta.X,
            "DeltaY" => _delta.Y,
            _ => throw new KeyNotFoundException($"Key '{key}' is not registered in MouseDevice")
        };
    }
}
