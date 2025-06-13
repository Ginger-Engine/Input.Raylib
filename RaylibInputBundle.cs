using Engine.Core;
using Engine.Input.Abstractions;
using GignerEngine.DiContainer;

namespace Engine.Input.RaylibInput;

public class RaylibInputBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<IInputSystem>().From<RaylibInputSystem>();
    }

}