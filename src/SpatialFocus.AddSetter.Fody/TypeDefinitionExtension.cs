// <copyright file="TypeDefinitionExtension.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Fody
{
	using Mono.Cecil;

	public static class TypeDefinitionExtension
	{
		public static string GetNamespace(this TypeDefinition type)
		{
			if (type.IsNested)
			{
				return type.DeclaringType.Namespace;
			}

			return type.Namespace;
		}
	}
}