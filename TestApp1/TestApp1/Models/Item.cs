using System;
using System.Net.Security;
using SQLite;

namespace TestApp1.Models
{
    public class Item
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int DictionaryId { get; set; }
        public string Word { get; set; }
        public string Transcription { get; set; }
        public string Translation { get; set; }
        public int Score { get; set; }
        public bool Studied { get; set; }
        public bool BeingStudied { get; set; }
    }
}