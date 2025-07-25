﻿using Engine.Core;
using Engine.Input.PointerEvents;
using Engine.Input.Raylib.Devices;
using GignerEngine.DiContainer;

namespace Engine.Input.Raylib;

public class RaylibInputBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<KeyboardDevice>();
        builder.Bind<MouseDevice>();
        builder.Bind<IPointerProvider>().From<RaylibPointerProvider>();
    }
}