using Entitas;

namespace TowerDefenceLike
{
    public enum SpeedFactor
    {
        x1 = 1, x2 = 2, x4 = 4
    }

    [Game]
    public class SpeedComponent : IComponent
    {
        public SpeedFactor value;
    }

    public static class SpeedFactorExtentions 
    {
        public static SpeedFactor Next(this SpeedFactor factor)
        {
            switch (factor)
            {
                case SpeedFactor.x1:
                    return SpeedFactor.x2;
                case SpeedFactor.x2:
                    return SpeedFactor.x4;
                case SpeedFactor.x4:
                default:
                    return SpeedFactor.x1;
            }
        }
        
        public static SpeedFactor Prev(this SpeedFactor factor)
        {
            switch (factor)
            {
                case SpeedFactor.x1:
                    return SpeedFactor.x4;
                case SpeedFactor.x4:
                    return SpeedFactor.x2;
                case SpeedFactor.x2:
                default:
                    return SpeedFactor.x1;
            }
        }
    }
}