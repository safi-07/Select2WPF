using AutoCompleteTextBox.Editors;
using Helpers.CustomControls.AutoComplete;
using Helpers.CustomControls.AutoComplete.Interfaces;
using Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AutoComplete
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISuggestionProvider
    {
        List<EntityViewModel> Entities = new List<EntityViewModel>();
        EntityViewModel Floor = new EntityViewModel();
        public MainWindow()
        {
            InitializeComponent();
            GetFloors();
            GeneralViewModel shelf = new GeneralViewModel();
            shelf.Entity = Entities.FirstOrDefault();
            DataContext = shelf;
            searchFloors.Provider = this;
        }
        public void GetFloors()
        {
            Entities.Add(new EntityViewModel() { Name = "Floor One" });
            Entities.Add(new EntityViewModel() { Name = "Floor Two" });
            Entities.Add(new EntityViewModel() { Name = "Floor Three" });
            Entities.Add(new EntityViewModel() { Name = "Floor Four" });
            Entities.Add(new EntityViewModel() { Name = "Floor Five" });
            Entities.Add(new EntityViewModel() { Name = "Floor Six" });
            Entities.Add(new EntityViewModel() { Name = "Floor Seven" });
            Entities.Add(new EntityViewModel() { Name = "Floor Eight" });
        }

        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return
                Entities
                    .Where(x => x.Name.Contains(filter) || string.IsNullOrEmpty(filter))
                    .ToList<IAutoCompleteItem>();
        }
    }
}
