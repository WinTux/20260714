using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cliente.Models
{
    public class Plato : INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }
        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set
            {
                if (_Nombre != value)
                {
                    _Nombre = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nombre)));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
