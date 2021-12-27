// <copyright file="ModuleWeaver.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace SpatialFocus.AddSetter.Fody
{
	using System.Collections.Generic;
	using System.Linq;
	using global::Fody;
	using Mono.Cecil;
	using Mono.Cecil.Cil;

	public partial class ModuleWeaver : BaseModuleWeaver
	{
		public override bool ShouldCleanReference => false;

		public override void Execute()
		{
			Namespaces namespaces = new Namespaces(this);
			References references = Fody.References.Init(this);

			foreach (TypeDefinition typeDefinition in ModuleDefinition.Types)
			{
				if (!typeDefinition.HasAddSetterAttribute(references) && !namespaces.ShouldIncludeType(typeDefinition))
				{
					continue;
				}

				WriteDebug($"Weaving type {typeDefinition}");

				foreach (PropertyDefinition propertyDefinition in typeDefinition.Properties)
				{
					TryWeaveSetterForProperty(typeDefinition, propertyDefinition, references);
				}
			}
		}

		public override IEnumerable<string> GetAssembliesForScanning()
		{
			yield return "netstandard";
			yield return "mscorlib";
			yield return "SpatialFocus.AddSetter";
		}

		private void TryWeaveSetterForProperty(TypeDefinition typeDefinition, PropertyDefinition propertyDefinition, References references)
		{
			bool isCompilerGenerated = propertyDefinition.GetMethod?.CustomAttributes.Any(x =>
				x.AttributeType.Resolve() == references.CompilerGeneratedAttributeType.Resolve()) ?? false;
			bool isSetMethodMissing = propertyDefinition.SetMethod == null;

			if (!isCompilerGenerated || !isSetMethodMissing)
			{
				return;
			}

			FieldDefinition backingField =
				typeDefinition.Fields.SingleOrDefault(x => x.Name == $"<{propertyDefinition.Name}>k__BackingField");

			if (backingField == null)
			{
				return;
			}

			WriteDebug($"Weaving property {typeDefinition}::{propertyDefinition.Name}");

			// Remove init only flags
			backingField.Attributes &= ~FieldAttributes.InitOnly;

			string setMethodName = "set_" + propertyDefinition.Name;
			MethodDefinition setter = new MethodDefinition(setMethodName,
				MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName, ModuleDefinition.TypeSystem.Void)
			{
				IsSetter = true, Body = { InitLocals = true, },
			};

			setter.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, propertyDefinition.PropertyType));

			ILProcessor ilProcessor = setter.Body.GetILProcessor();
			ilProcessor.Append(Instruction.Create(OpCodes.Ldarg_0));
			ilProcessor.Append(Instruction.Create(OpCodes.Ldarg_1));
			ilProcessor.Append(Instruction.Create(OpCodes.Stfld, backingField));
			ilProcessor.Append(Instruction.Create(OpCodes.Ret));

			propertyDefinition.SetMethod = setter;
			propertyDefinition.DeclaringType.Methods.Add(setter);
		}
	}
}