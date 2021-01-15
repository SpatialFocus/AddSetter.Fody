# SpatialFocus.AddSetter.Fody

An add setter [Fody](https://github.com/Fody/Home/) plugin.

[![Nuget](https://img.shields.io/nuget/v/SpatialFocus.AddSetter.Fody)](https://www.nuget.org/packages/SpatialFocus.MethodCache.Fody/)
[![Build & Publish](https://github.com/SpatialFocus/AddSetter.Fody/workflows/Build%20&%20Publish/badge.svg)](https://github.com/SpatialFocus/AddSetter.Fody/actions)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody?ref=badge_shield)

## Usage

See also [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md).

### NuGet installation

Install the [SpatialFocus.MethodCache.Fody NuGet package](https://nuget.org/packages/SpatialFocus.AddSetter.Fody/) and update the [Fody NuGet package](https://nuget.org/packages/Fody/):

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

## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FSpatialFocus%2FAddSetter.Fody?ref=badge_large)

----

Made with :heart: by [Spatial Focus](https://spatial-focus.net/)
