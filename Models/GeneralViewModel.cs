using Helpers.CustomControls.AutoComplete.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
        private EntityViewModel entity;

        public GeneralViewModel()
        {
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public EntityViewModel Entity
        {
            get { return entity; }
            set
            {
                entity = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Entity");
            }
        }

        public IAutoCompleteItem SelectedItem
        {
            set
            {
                if (value != null)
                {
                    if (value.GetType() == typeof(EntityViewModel))
                    {
                        Entity = (EntityViewModel)value;
                    }
                }
            }
        }

        #region[Property Changed Event]
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
