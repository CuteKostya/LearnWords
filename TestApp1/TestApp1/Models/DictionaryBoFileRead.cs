using System;
using System.Net.Security;
using SQLite;

namespace TestApp1.Models
{
    public class DictionaryBoFileRead
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MyLanguage { get; set; }
        public string LearningLanguage { get; set; }
        public WordBoFileRead[] Words { get; set; }
    }
}