// <copyright file="AddSetterBasicTests.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Tests
{
	using global::Fody;
	using SpatialFocus.AddSetter.Fody;
	using SpatialFocus.AddSetter.TestAssembly;
	using Xunit;

	[Collection("TestAssembly")]
	public class AddSetterBasicTests
	{
		static AddSetterBasicTests()
		{
			ModuleWeaver weavingTask = new ModuleWeaver();

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
	}
}