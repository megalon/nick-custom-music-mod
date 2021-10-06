using System;
using System.Reflection;

namespace NickCustomMusicMod.Utils
{
    public static class ReflectionUtil
	{
		public static object InvokeMethod(this object obj, string methodName, params object[] methodParams)
		{
			MethodInfo method = obj.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new InvalidOperationException(methodName + " is not a member of " + obj.GetType().Name);
			}
			return method.Invoke(obj, methodParams);
		}
	}
}
