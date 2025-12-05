using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSensorUseHandler : ItemUseHandlerBase
{
    public override string GetLabel(ItemUseContext ctx)
    {
        var sensor = ctx.sensorFeedback;
        if (sensor == null)
            return string.Empty; // disables button in your UI logic

        return sensor.IsOn ? "Turn off" : "Turn on";
    }

    public override bool CanUse(ItemUseContext ctx)
    {
        return true;
    }

    public override void Use(ItemUseContext ctx)
    {
        var sensor = ctx.sensorFeedback;
        if (sensor == null)
            return;

        if (sensor.IsOn)
            sensor.TurnOff();
        else
            sensor.TurnOn();
    }
}
