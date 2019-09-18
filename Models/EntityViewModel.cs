using Helpers.CustomControls.AutoComplete.Interfaces;
using System.ComponentModel;

namespace Models
{
    public class EntityViewModel : INotifyPropertyChanged, IAutoCompleteItem
    {
        #region [INotifyPrivateProperties]
        private string name { get; set; }
        #endregion

        public EntityViewModel()
        {
        }
        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Name");
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
        #region[Auto Complete Item]
        public string SearchText
        {
            get { return Name; }
        }


        #endregion
    }

}
