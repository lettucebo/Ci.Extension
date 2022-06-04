using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Ci.Extension
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the enum's description.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>Enum description</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

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
        /// Convert string to Enum by attribute
        /// </summary>
        /// <param name="value"></param>
        /// <param name="attributeType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if transform attribute is not supported</exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="TargetException"></exception>
        public static T ParseToEnumByAttribute<T>(this string value, string attributeType = "DisplayName") where T : struct
        {
            if (attributeType != "DisplayName" && attributeType != "Description")
            {
                throw new NotSupportedException($"{nameof(attributeType)} currently only support DisplayName or Description");
            }

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Only support for Enum parse");
            }

            switch (attributeType)
            {
                case "DisplayName":
                    return GetEnumByDisplayName<T>(value);
                case "Description":
                    return GetEnumByDescription<T>(value);
            }

            throw new TargetException($"{nameof(value)}: Can not match the enum. Not enum type.");
        }

        /// <summary>
        /// Try to convert string to Enum by attribute
        /// </summary>
        /// <param name="value"></param>
        /// <param name="attributeType"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if transform attribute is not supported</exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="TargetException"></exception>
        public static bool TryParseToEnumByAttribute<T>(this string value, string attributeType, out T result) where T : struct
        {
            if (attributeType != "DisplayName" && attributeType != "Description")
            {
                throw new NotSupportedException($"{nameof(attributeType)} currently only support DisplayName or Description");
            }

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Only support for Enum parse");
            }

            result = default;

            try
            {
                switch (attributeType)
                {
                    case "DisplayName":
                        result = GetEnumByDisplayName<T>(value);
                        return true;
                    case "Description":
                        result = GetEnumByDescription<T>(value);
                        return true;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                result = default;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }

        private static T GetEnumByDisplayName<T>(string value) where T : struct
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
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(value), "can not find match enum");
        }

        private static T GetEnumByDescription<T>(string value) where T : struct
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
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(value), "can not find match enum");
        }

        /// <summary>
        /// Check the enum is define or not
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>Enum description</returns>
        public static bool IsDefined(this Enum value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }
    }
}
