using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using SQLite;
using TestApp1.Annotations;

namespace TestApp1.Models
{
    public class Choice : INotifyPropertyChanged
    {
        private string _pressedButton;

        [PrimaryKey]
        public int Id { get; set; }
        public string Word { get; set; }
        public string[] MassTranslation { get; set; }
        public string Translation { get; set; }

        public string PressedButton
        {
            get => _pressedButton;
            set
            {
                if (value == _pressedButton)
                    return;
                _pressedButton = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}