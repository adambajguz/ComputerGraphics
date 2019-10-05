// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;

namespace ExampleGallery
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
            new ExampleDefinition("RGB Color cube", typeof(Direct3DInteropExample)),
        };
    }
}
