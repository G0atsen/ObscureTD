﻿namespace Keith.Towers{
    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,
    }

    public class TowerModifier
    {
        public readonly float Value;
        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source;

        public TowerModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public TowerModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
        public TowerModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
        public TowerModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}
