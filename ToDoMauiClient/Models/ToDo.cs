using System.ComponentModel;

namespace ToDoMauiClient.Models
{
    public class ToDo : INotifyPropertyChanged
    {
        private int _id;

        private string _todoName;

        public int Id
        {
            get => _id; set
            {
                if (_id == value) return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string TodoName
        {
            get => _todoName; set
            {
                if (_todoName == value) return;

                _todoName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TodoName)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}