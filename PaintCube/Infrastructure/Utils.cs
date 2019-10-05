// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleGallery
{
    static class Utils
    {
        public static List<T> GetEnumAsList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
