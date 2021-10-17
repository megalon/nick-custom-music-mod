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

        /// <summary>
        /// Gets the value of a private field. <paramref name="targetType"/> specifies the <see cref="Type"/> the field belongs to.
        /// </summary>
        /// <typeparam name="T">the type of the field (result casted)</typeparam>
        /// <param name="obj">the object instance to pull from</param>
        /// <param name="fieldName">the name of the field to read</param>
        /// <param name="targetType">the object <see cref="Type"/> the field belongs to</param>
        /// <returns>the value of the field</returns>
        /// <exception cref="InvalidOperationException">thrown when <paramref name="fieldName"/> is not a member of <paramref name="obj"/></exception>
        /// <exception cref="ArgumentException">thrown when <paramref name="obj"/> isn't assignable as <paramref name="targetType"/></exception>
        public static T GetPrivateField<T>(this object obj, string fieldName, Type targetType)
        {
            var prop = targetType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop == null)
                throw new InvalidOperationException($"{fieldName} is not a member of {targetType.Name}");
            var value = prop.GetValue(obj);
            return (T)value;
        }

        /// <summary>
        /// Gets the value of a private field.
        /// </summary>
        /// <typeparam name="T">the type of te field (result casted)</typeparam>
        /// <param name="obj">the object instance to pull from</param>
        /// <param name="fieldName">the name of the field to read</param>
        /// <returns>the value of the field</returns>
        /// <exception cref="InvalidOperationException">thrown when <paramref name="fieldName"/> is not a member of <paramref name="obj"/></exception>
        public static T GetPrivateField<T>(this object obj, string fieldName) => obj.GetPrivateField<T>(fieldName, obj.GetType());
    }
}
