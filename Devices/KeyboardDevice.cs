using Engine.Input.Devices;
using Raylib_cs;

public class KeyboardDevice : IInputDevice
{
    public string Id => "Keyboard";

    public Dictionary<string, InputValueType> Keys { get; } = new()
    {
        ["W"] = InputValueType.Bool,
        ["A"] = InputValueType.Bool,
        ["S"] = InputValueType.Bool,
        ["D"] = InputValueType.Bool,
        ["Space"] = InputValueType.Bool,
        ["CtrlLeft"] = InputValueType.Bool,
        ["Tab"] = InputValueType.Bool,
        ["R"] = InputValueType.Bool,
        ["Q"] = InputValueType.Bool,
        ["E"] = InputValueType.Bool,
    };

    private readonly Dictionary<string, KeyboardKey> _keyMap = new()
    {
        ["W"] = KeyboardKey.W,
        ["A"] = KeyboardKey.A,
        ["S"] = KeyboardKey.S,
        ["D"] = KeyboardKey.D,
        ["Space"] = KeyboardKey.Space,
        ["CtrlLeft"] = KeyboardKey.LeftControl,
        ["Tab"] = KeyboardKey.Tab,
        ["R"] = KeyboardKey.R,
        ["Q"] = KeyboardKey.Q,
        ["E"] = KeyboardKey.E,
    };

    public event Action<string>? OnPressed;
    public event Action<string>? OnReleased;

    private readonly Dictionary<string, bool> _prevState = new();
    private readonly Dictionary<string, bool> _currState = new();

    public void Update()
    {
        foreach (var (name, key) in _keyMap)
        {
            bool isDown = Raylib.IsKeyDown(key);
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
        return _currState.TryGetValue(key, out var value) ? value : false;
    }
}