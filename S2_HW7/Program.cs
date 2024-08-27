namespace S2_HW7
{
    using System;
    using System.Linq;
    using System.Reflection;

    // Создаем атрибут CustomName
    [AttributeUsage(AttributeTargets.Field)]

    public class CustomNameAttribute : Attribute
    {
        public string CustomFieldName { get; }

        public CustomNameAttribute(string customFieldName)
        {
            CustomFieldName = customFieldName;
        }
    }

    // Создаем класс с методами для сериализации и десериализации
    public class SerializationHelper
    {
        // Метод для сериализации объекта в строку
        public static string ObjectToString(object obj)
        {
            Type type = obj.GetType();
            string result = "";

            foreach (FieldInfo field in type.GetFields())
            {
                string fieldName = field.Name;
                string fieldValue = field.GetValue(obj)?.ToString() ?? "null"; // Обработка null

                // Проверяем наличие атрибута CustomName
                CustomNameAttribute attribute = (CustomNameAttribute)Attribute.GetCustomAttribute(field, typeof(CustomNameAttribute));
                if (attribute != null)
                {
                    fieldName = attribute.CustomFieldName;
                }

                result += $"{fieldName}:{fieldValue}\n";
            }

            return result;
        }

        // Метод для десериализации строки в объект
        public static void StringToObject(string data, object obj)
        {
            string[] lines = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries); // Убираем пустые строки

            foreach (string line in lines)
            {
                string[] parts = line.Split(':', 2); // Ограничиваем количество частей до 2
                if (parts.Length < 2) continue; // Пропускаем строки без значения

                string fieldName = parts[0];
                string fieldValue = parts[1];

                // Поиск поля с атрибутом CustomName
                FieldInfo field = obj.GetType().GetField(fieldName);
                if (field == null)
                {
                    field = obj.GetType().GetFields().FirstOrDefault(f =>
                    {
                        CustomNameAttribute attribute = (CustomNameAttribute)Attribute.GetCustomAttribute(f, typeof(CustomNameAttribute));
                        return attribute != null && attribute.CustomFieldName == fieldName;
                    });
                }

                if (field != null)
                {
                    field.SetValue(obj, Convert.ChangeType(fieldValue, field.FieldType));
                }
            }
        }
    }

    // Пример использования
    public class MyClass
    {
        [CustomName("customfieldname")]
        public int i = 0;
        public string name = "John";
    }

    public class Program
    {
        public static void Main()
        {
            MyClass obj = new MyClass();
            string serializedData = SerializationHelper.ObjectToString(obj);
            Console.WriteLine(serializedData);

            MyClass newObj = new MyClass();
            SerializationHelper.StringToObject(serializedData, newObj);
            Console.WriteLine($"New object: i={newObj.i}, name={newObj.name}");
        }
    }
}