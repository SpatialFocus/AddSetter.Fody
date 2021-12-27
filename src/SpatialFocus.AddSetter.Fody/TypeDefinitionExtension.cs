// <copyright file="TypeDefinitionExtension.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Fody
{
	using System;
	using System.Linq;
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

		public static bool HasAddSetterAttribute(this TypeDefinition typeDefinition, References references)
		{
			if (typeDefinition == null)
			{
				throw new ArgumentNullException(nameof(typeDefinition));
			}

			if (references == null)
			{
				throw new ArgumentNullException(nameof(references));
			}

			TypeReference addSetterAttributeType = references.AddSetterAttributeType.Resolve();

			return typeDefinition.CustomAttributes.Any(classAttribute =>
				classAttribute.AttributeType.Resolve().Equals(addSetterAttributeType));
		}
	}
}