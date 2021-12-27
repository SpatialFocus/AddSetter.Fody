// <copyright file="AddSetterAttribute.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

using System;

[assembly: CLSCompliant(false)]

namespace SpatialFocus.AddSetter
{
	using System;

	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class AddSetterAttribute : Attribute
	{
		public AddSetterAttribute()
		{
		}
	}
}