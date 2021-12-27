# SpatialFocus.AddSetter.Fody

An add setter [Fody](https://github.com/Fody/Home/) plugin.

[![Nuget](https://img.shields.io/nuget/v/SpatialFocus.AddSetter.Fody)](https://www.nuget.org/packages/SpatialFocus.AddSetter.Fody/)
[![Build & Publish](https://github.com/SpatialFocus/AddSetter.Fody/workflows/Build%20&%20Publish/badge.svg)](https://github.com/SpatialFocus/AddSetter.Fody/actions)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody?ref=badge_shield)

Adds private setter to properties. To be used for example in combination with read-only properties in EF Core, that would otherwise [not be mapped by convention](https://docs.microsoft.com/en-us/ef/core/modeling/constructors#read-only-properties). Read more in our [blog post](https://www.spatial-focus.net/blog/removing-infrastructure-information-from-domain-code-3).

## Usage

See also [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md).

### NuGet installation

Install the [SpatialFocus.AddSetter.Fody NuGet package](https://nuget.org/packages/SpatialFocus.AddSetter.Fody/) and update the [Fody NuGet package](https://nuget.org/packages/Fody/):

```powershell
PM> Install-Package Fody
PM> Install-Package SpatialFocus.AddSetter.Fody
```

The `Install-Package Fody` is required since NuGet always defaults to the oldest, and most buggy, version of any dependency.

### Add to FodyWeavers.xml

Add `<SpatialFocus.AddSetter/>` to [FodyWeavers.xml](https://github.com/Fody/Home/blob/master/pages/usage.md#add-fodyweaversxml)

```xml
<Weavers>
    <SpatialFocus.AddSetter/>
</Weavers>
```

## Overview

Before code:

```csharp
public class Person
{
    public int Id { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
```

What gets compiled

```csharp
public class Person
{
    public int Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }
}
```

# Configuration Options

## Do not include by default

If no include or exclude namespaces are defined, all classes in the package are __included by default__. To change this behavior, add the following attribute to the `SpatialFocus.AddSetter` node in FodyWeavers.

```xml
<Weavers>
    <SpatialFocus.AddSetter DoNotIncludeByDefault='True'/>
</Weavers>
```

## Include types with an attribute

To include a specific class you can mark it with a `AddSetter`. This works in both of the following cases, when either the `DoNotIncludeByDefault` setting is turned on, or the namespace is excluded.

```csharp
[AddSetter]
public class ClassToInclude
{
    ...
}
```

## Include or exclude namespaces

These config options are configured by modifying the `SpatialFocus.AddSetter` node in FodyWeavers.xml

### ExcludeNamespaces

A list of namespaces to exclude.

Can take two forms.

As an element with items delimited by a newline.

```xml
<SpatialFocus.AddSetter>
    <ExcludeNamespaces>
        Foo
        Bar
    </ExcludeNamespaces>
</SpatialFocus.AddSetter>
```

Or as a attribute with items delimited by a pipe `|`.

```xml
<SpatialFocus.AddSetter ExcludeNamespaces='Foo|Bar'/>
```

### IncludeNamespaces

A list of namespaces to include.

Can take two forms.

As an element with items delimited by a newline.

```xml
<SpatialFocus.AddSetter>
    <IncludeNamespaces>
        Foo
        Bar
    </IncludeNamespaces>
</SpatialFocus.AddSetter>
```

Or as a attribute with items delimited by a pipe `|`.

```xml
<SpatialFocus.AddSetter IncludeNamespaces='Foo|Bar'/>
```

### Wildcard support

Use `*` at the beginning or at the end of an in- or exclude for wildcard matching.

To include the namespace and all sub-namespaces, simply define it like this:

```xml
<SpatialFocus.AddSetter>
    <IncludeNamespaces>
        Foo
        Foo.*
    </IncludeNamespaces>
</SpatialFocus.AddSetter>
```

### Combination of exclude and include

You can combine excludes and includes, excludes overrule the includes if both match. But classes with an explicit `[AddSetter]` attribute overrule the excludes.

## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody?ref=badge_large)

----

Made with :heart: by [Spatial Focus](https://spatial-focus.net/)
