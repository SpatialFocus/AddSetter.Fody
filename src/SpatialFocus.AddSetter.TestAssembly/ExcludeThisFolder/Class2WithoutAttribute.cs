// <copyright file="Class2WithoutAttribute.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.TestAssembly.ExcludeThisFolder
{
	public class Class2WithoutAttribute
	{
		// ReSharper disable once UnassignedGetOnlyAutoProperty
		public string Test { get; }

#pragma warning disable CA1822 // Mark members as static
		public string Test2 => "Test";
#pragma warning restore CA1822 // Mark members as static

		public string Test3 { get; set; }
	}
}