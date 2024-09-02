using System;
using System.Text.Json;

namespace S3_HW3
{
    internal class Message
    {
        public string? Name { get; init; }
        public string? Text { get; init; }
        public DateTime Stime { get; }

        public Message(string nikname, string text)
        {
            Name = nikname;
            Text = text;
            Stime = DateTime.Now;
        }

        public string ToJson() => JsonSerializer.Serialize(this);

        public static Message? FromJson(string somemessage) =>
            JsonSerializer.Deserialize<Message>(somemessage);

        public override string ToString() =>
            $" Получено сообщение от {Name} ({Stime.ToShortTimeString()}): \n {Text}";
    }
}