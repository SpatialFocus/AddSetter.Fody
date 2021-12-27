// <copyright file="AddSetterBasicTests.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Tests
{
	using System.Xml.Linq;
	using global::Fody;
	using SpatialFocus.AddSetter.Fody;
	using SpatialFocus.AddSetter.TestAssembly;
	using SpatialFocus.AddSetter.TestAssembly.ExcludeThisFolder;
	using Xunit;

	[Collection("TestAssembly")]
	public class AddSetterBasicTests
	{
		static AddSetterBasicTests()
		{
			XElement xElement = XElement.Parse("<AddSetter ExcludeNamespaces='SpatialFocus.AddSetter.TestAssembly.ExcludeThisFolder' />");

			ModuleWeaver weavingTask = new ModuleWeaver { Config = xElement, };

			AddSetterBasicTests.TestResult =
				weavingTask.ExecuteTestRun("SpatialFocus.AddSetter.TestAssembly.dll", ignoreCodes: new[] { "0x80131869" });
		}

		private static TestResult TestResult { get; }

		[Fact]
		public void Test()
		{
			object class1 = TestHelpers.CreateInstance<Class1>(AddSetterBasicTests.TestResult.Assembly);
			Assert.True(class1.GetType().GetProperty(nameof(Class1.Test))?.CanWrite);
			Assert.False(class1.GetType().GetProperty(nameof(Class1.Test2))?.CanWrite);
			Assert.True(class1.GetType().GetProperty(nameof(Class1.Test3))?.CanWrite);
		}

		[Fact]
		public void TestExcludedNamespaceWithAddSetterAttribute()
		{
			object class1 = TestHelpers.CreateInstance<Class2WithAttribute>(AddSetterBasicTests.TestResult.Assembly);
			Assert.True(class1.GetType().GetProperty(nameof(Class2WithAttribute.Test))?.CanWrite);
			Assert.False(class1.GetType().GetProperty(nameof(Class2WithAttribute.Test2))?.CanWrite);
			Assert.True(class1.GetType().GetProperty(nameof(Class2WithAttribute.Test3))?.CanWrite);
		}

		[Fact]
		public void TestExcludedNamespaceWithoutAttribute()
		{
			object class1 = TestHelpers.CreateInstance<Class2WithoutAttribute>(AddSetterBasicTests.TestResult.Assembly);
			Assert.False(class1.GetType().GetProperty(nameof(Class2WithoutAttribute.Test))?.CanWrite);
			Assert.False(class1.GetType().GetProperty(nameof(Class2WithoutAttribute.Test2))?.CanWrite);
			Assert.True(class1.GetType().GetProperty(nameof(Class2WithoutAttribute.Test3))?.CanWrite);
		}
	}
}