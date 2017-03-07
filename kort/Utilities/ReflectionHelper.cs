using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities
{
	public static class ReflectionHelper
    {
		public static List<Type> GetChildTypes(Type parentType)
		{
			Assembly assembly = parentType.Assembly;

			Type[] allTypes = assembly.GetTypes();

			List<Type> typesInheritingFromParent = allTypes.Where(type => parentType.IsAssignableFrom(type) && type != parentType).ToList();

			return typesInheritingFromParent;
		}
	}
}
