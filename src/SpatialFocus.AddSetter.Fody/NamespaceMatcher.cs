// <copyright file="NamespaceMatcher.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Fody
{
	using System;
	using Mono.Cecil;

	public class NamespaceMatcher
	{
		private readonly bool starEnd;
		private readonly bool starStart;

		public NamespaceMatcher(string line)
		{
			if (line == null)
			{
				throw new ArgumentNullException(nameof(line));
			}

			Line = line;

			if (Line.StartsWith("*", StringComparison.Ordinal))
			{
				this.starStart = true;
				Line = Line.Substring(1);
			}

			if (Line.EndsWith("*", StringComparison.Ordinal))
			{
				this.starEnd = true;
				Line = Line.Substring(0, Line.Length - 1);
			}

			Validate();
		}

		public string Line { get; }

		public bool Match(TypeDefinition typeDefinition)
		{
			if (typeDefinition == null)
			{
				throw new ArgumentNullException(nameof(typeDefinition));
			}

			string typeName = typeDefinition.GetNamespace();

			if (this.starStart && this.starEnd)
			{
				return typeName.Contains(Line);
			}

			if (this.starStart)
			{
				return typeName.EndsWith(Line, StringComparison.Ordinal);
			}

			if (this.starEnd)
			{
				return typeName.StartsWith(Line, StringComparison.Ordinal);
			}

			return typeName == Line;
		}

		private void Validate()
		{
			if (Line.Contains("*"))
			{
				throw new Exception("Namespaces can't only start or end with '*'.");
			}

			if (Line.Contains(" "))
			{
				throw new Exception("Namespaces cant contain spaces.");
			}
		}
	}
}