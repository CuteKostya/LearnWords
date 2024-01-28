using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TestApp1.Models
{
    public class DictionaryBo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MyLanguage { get; set; }
        public string LearningLanguage { get; set; }
    }
}