// <copyright file="References.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Fody
{
	using System;
	using System.Runtime.CompilerServices;
	using Mono.Cecil;

	public class References
	{
		protected References(ModuleWeaver moduleWeaver)
		{
			ModuleWeaver = moduleWeaver;
		}

		public TypeReference CompilerGeneratedAttributeType { get; set; }

		protected ModuleWeaver ModuleWeaver { get; }

		public static References Init(ModuleWeaver moduleWeaver)
		{
			if (moduleWeaver == null)
			{
				throw new ArgumentNullException(nameof(moduleWeaver));
			}

			References references = new References(moduleWeaver);

			TypeDefinition compilerGeneratedAttributeType = moduleWeaver.FindTypeDefinition(typeof(CompilerGeneratedAttribute).FullName);
			references.CompilerGeneratedAttributeType = moduleWeaver.ModuleDefinition.ImportReference(compilerGeneratedAttributeType);

			return references;
		}
	}
}