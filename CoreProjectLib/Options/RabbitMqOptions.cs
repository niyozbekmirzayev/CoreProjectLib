﻿
namespace CoreProjectLib.Options
{
    public class RabbitMqOptions
    {
        public string VirtualHost { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
    }
}
