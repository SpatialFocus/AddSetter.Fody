// <copyright file="Namespaces.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Fody
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;
	using Mono.Cecil;

	public class Namespaces
	{
		private readonly ICollection<NamespaceMatcher> excludeNamespaces = new List<NamespaceMatcher>();
		private readonly ICollection<NamespaceMatcher> includeNamespaces = new List<NamespaceMatcher>();

		public Namespaces(ModuleWeaver moduleWeaver)
		{
			ModuleWeaver = moduleWeaver;

			ReadIncludes();
			ReadExcludes();
		}

		public IReadOnlyCollection<NamespaceMatcher> ExcludeNamespaces => this.excludeNamespaces.ToList().AsReadOnly();

		public IReadOnlyCollection<NamespaceMatcher> IncludeNamespaces => this.includeNamespaces.ToList().AsReadOnly();

		protected ModuleWeaver ModuleWeaver { get; }

		public bool ShouldIncludeType(TypeDefinition typeDefinition)
		{
			return (!this.includeNamespaces.Any() || this.includeNamespaces.Any(x => x.Match(typeDefinition))) &&
				!this.excludeNamespaces.Any(x => x.Match(typeDefinition));
		}

		private void ReadExcludes()
		{
			XAttribute excludeNamespacesAttribute = ModuleWeaver.Config.Attribute("ExcludeNamespaces");

			if (excludeNamespacesAttribute != null)
			{
				foreach (string item in excludeNamespacesAttribute.Value.Split('|')
					.Select(x => x.Trim())
					.Where(x => !string.IsNullOrEmpty(x)))
				{
					this.excludeNamespaces.Add(new NamespaceMatcher(item));
				}
			}

			XElement excludeNamespacesElement = ModuleWeaver.Config.Element("ExcludeNamespaces");

			if (excludeNamespacesElement != null)
			{
				foreach (string item in excludeNamespacesElement.Value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
					.Select(x => x.Trim())
					.Where(x => !string.IsNullOrEmpty(x)))
				{
					this.excludeNamespaces.Add(new NamespaceMatcher(item));
				}
			}
		}

		private void ReadIncludes()
		{
			XAttribute includeNamespacesAttribute = ModuleWeaver.Config.Attribute("IncludeNamespaces");

			if (includeNamespacesAttribute != null)
			{
				foreach (string item in includeNamespacesAttribute.Value.Split('|')
					.Select(x => x.Trim())
					.Where(x => !string.IsNullOrEmpty(x)))
				{
					this.includeNamespaces.Add(new NamespaceMatcher(item));
				}
			}

			XElement includeNamespacesElement = ModuleWeaver.Config.Element("IncludeNamespaces");

			if (includeNamespacesElement != null)
			{
				foreach (string item in includeNamespacesElement.Value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
					.Select(x => x.Trim())
					.Where(x => !string.IsNullOrEmpty(x)))
				{
					this.includeNamespaces.Add(new NamespaceMatcher(item));
				}
			}
		}
	}
}