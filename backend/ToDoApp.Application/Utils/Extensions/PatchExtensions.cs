using System.Reflection;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Application.Utils.Extensions
{
    public static class PatchExtensions
    {
        public static bool ApplyChanges<TEntity, TInput>(this TEntity entity, TInput input)
        {
            bool hasChanges = false;

            var entityProps = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                             .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in entityProps)
            {
                var inputProp = typeof(TInput).GetProperty(prop.Name);
                if (inputProp == null) continue;

                var oldValue = prop.GetValue(entity);
                var newValue = inputProp.GetValue(input);

                if (newValue == null) continue;

                var type = prop.PropertyType;
                if (!IsSimpleType(type))
                    continue;

                if (!Equals(oldValue, newValue))
                {
                    prop.SetValue(entity, newValue);
                    hasChanges = true;
                }
            }

            return hasChanges;
        }

        private static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                type.IsEnum ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(DateTime) ||
                type == typeof(Guid) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(TimeSpan) ||
                (Nullable.GetUnderlyingType(type) != null && IsSimpleType(Nullable.GetUnderlyingType(type)));
        }
    }
}
