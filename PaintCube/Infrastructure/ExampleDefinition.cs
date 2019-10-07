using System;

namespace PaintCube
{
    public class ExampleDefinition
    {
        public ExampleDefinition(string name, Type control)
        {
            Name = name;
            Control = control;
        }

        public string Name { get; private set; }
        public Type Control { get; private set; }
    }

    public class ExampleDefinitions
    {
        public static ExampleDefinition[] Definitions { get; } = {
            new ExampleDefinition("Paint", typeof(SvgExample)),
            new ExampleDefinition("RGB <-> CMYK converter", typeof(ColorConverter)),
            new ExampleDefinition("RGB Color cube", typeof(Direct3DInteropExample)),
        };
    }
}
