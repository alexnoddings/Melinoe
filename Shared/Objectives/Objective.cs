using System;

namespace Melinoe.Shared.Objectives
{
    [Flags]
    public enum Objective
    {
        GhostEvent = 1 << 1,
        Photo = 1 << 2,
        EmfReader = 1 << 3,
        MotionSensor = 1 << 4,
        SmudgeArea = 1 << 5,
        Crucifix = 1 << 6,
        Salt = 1 << 7,
        Candle = 1 << 8,
        EscapeNoDeaths = 1 << 9,
        SmudgeHunt = 1 << 10,
        LowSanity = 1 << 11,
    }
}
