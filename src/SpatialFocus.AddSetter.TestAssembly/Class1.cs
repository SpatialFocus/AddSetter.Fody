// <copyright file="Class1.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.TestAssembly
{
	public class Class1
	{
		// ReSharper disable once UnassignedGetOnlyAutoProperty
		public string Test { get; }

#pragma warning disable CA1822 // Mark members as static
		public string Test2 => "Test";
#pragma warning restore CA1822 // Mark members as static

		public string Test3 { get; set; }
	}
}