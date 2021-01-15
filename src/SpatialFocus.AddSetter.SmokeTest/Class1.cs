// <copyright file="Class1.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

using System;

[assembly: CLSCompliant(false)]

namespace SpatialFocus.AddSetter.SmokeTest
{
	public class Entity
	{
		public string Test { get; }

		public string Test2 => "Test";
	}
}