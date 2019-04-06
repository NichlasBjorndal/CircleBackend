using System;
using circle_backend.Models;
using Newtonsoft.Json;

namespace circle_backend.Utilities
{
    public class JsonSerializer
    {
        public static string TextMessageToJson(TextMessage textMessage)
        {
            return JsonConvert.SerializeObject(textMessage);
        }

        public static TextMessage JsonToTextMessage(string jsonTextMessage)
        {
            return (TextMessage)JsonConvert.DeserializeObject(jsonTextMessage);
        }

        public static string DrawingMessageToJson(DrawingMessage drawingMessage)
        {
            return JsonConvert.SerializeObject(drawingMessage);
        }

        public static DrawingMessage JsonToDrawingMessage(string jsonDrawingMessage)
        {
            return (DrawingMessage)JsonConvert.DeserializeObject(jsonDrawingMessage);
        }
    }
}
