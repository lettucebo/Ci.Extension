using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ci.Extension.Core
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the Enum's description.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>Enum description</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Retrieves the <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" />
        /// of the current enum value, or the enum's member name if the <see cref="DisplayAttribute" /> is not present.
        /// </summary>
        /// <param name="value">This enum member to get the name for.</param>
        /// <returns>The <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" /> attribute, if present.</returns>
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType()
                       .GetMember(value.ToString())
                       .FirstOrDefault()
                       ?.GetCustomAttribute<DisplayAttribute>(false)
                       ?.Name
                   ?? value.ToString();
        }

        /// <summary>
        /// Convert string to Enum by attritube
        /// </summary>
        /// <param name="value"></param>
        /// <param name="attritubeType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if transform attritube is not supported</exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="TargetException"></exception>
        public static T ParseToEnumByAttritube<T>(this string value, string attritubeType = "DisplayName")
        {
            if (attritubeType != "DisplayName" && attritubeType != "Description")
            {
                throw new ArgumentException($"{nameof(attritubeType)} can only be DisplayName or Description",
                    nameof(attritubeType));
            }

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Only support for Enum parse");
            }

            switch (attritubeType)
            {
                case "DisplayName":
                    return GetEnumByDisplayName<T>(value);
                case "Description":
                    return GetEnumByDescription<T>(value);
            }

            throw new TargetException($"{nameof(value)}: Can not match the enum. Not enum type.");
        }

        private static T GetEnumByDisplayName<T>(string value)
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DisplayAttribute)) as DisplayAttribute;
                if (attribute != null)
                {
                    if (attribute.Name == value)
                    {
                        return (T) field.GetValue(null);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(value), "can not find match enum");
        }

        private static T GetEnumByDescription<T>(string value)
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == value)
                    {
                        return (T) field.GetValue(null);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(value), "can not find match enum");
        }
    }
}