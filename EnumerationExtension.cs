using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;

namespace BasicWpfLibrary
{
    // https://stackoverflow.com/questions/58743/databinding-an-enum-property-to-a-combobox-in-wpf
    public class EnumerationExtension : MarkupExtension
    {
        private readonly Type _enumType;


        public EnumerationExtension(Type? enumType)
        {
            EnumType = enumType ?? throw new ArgumentNullException("enumType");
            _enumType = enumType;
        }

        public Type EnumType
        {
            get => _enumType;
            private init
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider) // or IXamlServiceProvider for UWP and WinUI
        {
            var enumValues = Enum.GetValues(EnumType);

            return (
                from object enumValue in enumValues
                select new EnumerationMember
                {
                    Value = enumValue,
                    Description = GetDescription(enumValue)
                }).ToArray();
        }

        private string? GetDescription(object? enumValue)
        {
            if(enumValue == null)
                return string.Empty;
            
            return EnumType
                .GetField(enumValue.ToString() ?? "Unknown")
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() is DescriptionAttribute descriptionAttribute
                ? descriptionAttribute.Description
                : enumValue.ToString();
        }

        public class EnumerationMember
        {
            public string? Description { get; set; }
            public object? Value { get; set; }
        }
    }
}
