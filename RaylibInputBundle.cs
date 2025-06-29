using Engine.Core;
using Engine.Input.RaylibInput.Devices;
using GignerEngine.DiContainer;

namespace Engine.Input.RaylibInput;

public class RaylibInputBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<KeyboardDevice>();
        builder.Bind<MouseDevice>();
    }
}