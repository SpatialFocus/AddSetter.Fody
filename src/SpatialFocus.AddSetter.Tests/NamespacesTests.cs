// <copyright file="NamespacesTests.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Tests
{
	using System.Linq;
	using System.Xml.Linq;
	using Mono.Cecil;
	using SpatialFocus.AddSetter.Fody;
	using Xunit;

	public class NamespacesTests
	{
		[Fact]
		public void DoNotIncludeByDefaultFalse()
		{
			XElement xElement = XElement.Parse("<AddSetter DoNotIncludeByDefault='False' />");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement };
			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.False(namespaces.DoNotIncludeByDefault);
		}

		[Fact]
		public void DoNotIncludeByDefaultNotSet()
		{
			XElement xElement = XElement.Parse("<AddSetter />");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement };
			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.False(namespaces.DoNotIncludeByDefault);
		}

		[Fact]
		public void DoNotIncludeByDefaultTrue()
		{
			XElement xElement = XElement.Parse("<AddSetter DoNotIncludeByDefault='True' />");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement };
			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.True(namespaces.DoNotIncludeByDefault);
		}

		[Fact]
		public void ExcludeNamespacesAttribute()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter ExcludeNamespaces='Foo|Bar' />");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Foo", namespaces.ExcludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Bar", namespaces.ExcludeNamespaces.ElementAt(1).Line);
		}

		[Fact]
		public void ExcludeNamespacesCombined()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter  ExcludeNamespaces='Foo'>
    <ExcludeNamespaces>
Bar
    </ExcludeNamespaces>
</AddSetter>");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Foo", namespaces.ExcludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Bar", namespaces.ExcludeNamespaces.ElementAt(1).Line);
		}

		[Fact]
		public void ExcludeNamespacesNode()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter>
    <ExcludeNamespaces>
Foo
Bar
Foo.Bar
    </ExcludeNamespaces>
</AddSetter>");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Foo", namespaces.ExcludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Bar", namespaces.ExcludeNamespaces.ElementAt(1).Line);
			Assert.Equal("Foo.Bar", namespaces.ExcludeNamespaces.ElementAt(2).Line);
		}

		[Fact]
		public void IncludeAndExcludeNamespacesAttribute()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter IncludeNamespaces='Bar' ExcludeNamespaces='Foo' />");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Bar", namespaces.IncludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Foo", namespaces.ExcludeNamespaces.ElementAt(0).Line);
		}

		[Fact]
		public void IncludeNamespacesAttribute()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter IncludeNamespaces='Foo|Bar' />");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Foo", namespaces.IncludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Bar", namespaces.IncludeNamespaces.ElementAt(1).Line);
		}

		[Fact]
		public void IncludeNamespacesCombined()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter  IncludeNamespaces='Foo'>
    <IncludeNamespaces>
Bar
    </IncludeNamespaces>
</AddSetter>");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Foo", namespaces.IncludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Bar", namespaces.IncludeNamespaces.ElementAt(1).Line);
		}

		[Fact]
		public void IncludeNamespacesNode()
		{
			XElement xElement = XElement.Parse(@"
<AddSetter>
    <IncludeNamespaces>
Foo
Bar
Foo.Bar
    </IncludeNamespaces>
</AddSetter>");
			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.Equal("Foo", namespaces.IncludeNamespaces.ElementAt(0).Line);
			Assert.Equal("Bar", namespaces.IncludeNamespaces.ElementAt(1).Line);
			Assert.Equal("Foo.Bar", namespaces.IncludeNamespaces.ElementAt(2).Line);
		}

		[Fact]
		public void ShouldIncludeTypeIfIncludedAndNotExcluded()
		{
			XElement xElement = XElement.Parse("<AddSetter IncludeNamespaces='Foo*|*Foo.Bar.Foo*' ExcludeNamespaces='Foo.Bar*' />");

			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo", "Hugo", TypeAttributes.Class)));
			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo1", "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo.Bar", "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo.Bar1", "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo.Bar.Foo", "Hugo", TypeAttributes.Class)));
		}

		[Fact]
		public void ShouldIncludeTypeIfNotExcluded()
		{
			XElement xElement = XElement.Parse("<AddSetter ExcludeNamespaces='Foo.Bar*' />");

			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo", "Hugo", TypeAttributes.Class)));
			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo1", "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo.Bar", "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo.Bar1", "Hugo", TypeAttributes.Class)));
		}

		[Fact]
		public void ShouldIncludeTypeIncludesAllByDefault()
		{
			XElement xElement = XElement.Parse("<AddSetter />");

			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition(string.Empty, "Hugo", TypeAttributes.Class)));
			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo", "Hugo", TypeAttributes.Class)));
			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo1", "Hugo", TypeAttributes.Class)));
		}

		[Fact]
		public void ShouldIncludeTypeWorksWithDefaultInclude()
		{
			XElement xElement = XElement.Parse("<AddSetter IncludeNamespaces='*' />");

			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition(string.Empty, "Hugo", TypeAttributes.Class)));
			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo", "Hugo", TypeAttributes.Class)));
			Assert.True(namespaces.ShouldIncludeType(new TypeDefinition("Foo1", "Hugo", TypeAttributes.Class)));
		}

		[Fact]
		public void ShouldNotIncludeTypeByDefault()
		{
			XElement xElement = XElement.Parse("<AddSetter DoNotIncludeByDefault='True' />");

			ModuleWeaver moduleWeaver = new ModuleWeaver { Config = xElement, };

			Namespaces namespaces = new Namespaces(moduleWeaver);

			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition(string.Empty, "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo", "Hugo", TypeAttributes.Class)));
			Assert.False(namespaces.ShouldIncludeType(new TypeDefinition("Foo1", "Hugo", TypeAttributes.Class)));
		}
	}
}