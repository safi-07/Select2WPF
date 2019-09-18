using AutoCompleteTextBox.Editors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Helpers.CustomControls.AutoComplete.Interfaces;

namespace Helpers.CustomControls.AutoComplete
{
    #region[Delegates]
    public delegate void AutoCompleteSelectEventHandler(CustomAutoCompleteTextBox sender, object item, bool autoSelection);
    #endregion
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>
    public partial class CustomAutoCompleteTextBox : UserControl
    {
        #region[Private Properties]
        #endregion
        #region[Constructor]
        public CustomAutoCompleteTextBox()
        {
            InitializeComponent();
        }
        #endregion
        #region[Dependency Property]
        public static readonly DependencyProperty ProviderProperty = DependencyProperty.Register("Provider", typeof(ISuggestionProvider), typeof(CustomAutoCompleteTextBox), new PropertyMetadata(onProviderChanged));
        public ISuggestionProvider Provider
        {
            get { return GetValue(ProviderProperty) as ISuggestionProvider; }
            set { SetValue(ProviderProperty, value); }
        }
        private static void onProviderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CustomAutoCompleteTextBox).SuggestionsBox.Provider = (e.NewValue as ISuggestionProvider);
        }

        private static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register("SelectedText", typeof(IAutoCompleteItem), typeof(CustomAutoCompleteTextBox), new PropertyMetadata(SelectedTextChanged));
        public IAutoCompleteItem SelectedText
        {
            get { return GetValue(SelectedTextProperty) as IAutoCompleteItem; }
            set { SetValue(SelectedTextProperty, value); }
        }
        private static void SelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomAutoCompleteTextBox sender = d as CustomAutoCompleteTextBox;
            sender.SupportiveTextBox.Text = ((IAutoCompleteItem)e.NewValue).SearchText;
        }
        #endregion

        #region[Events]
        private void SupportiveTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab)
            {
                e.Handled = true;
                return;
            }
            if (e.Key == Key.Enter)
            {
                ChangeAutoCompleteVisibility();
            }

        }
        private void SupportiveTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeAutoCompleteVisibility();
            e.Handled = true;
        }
        private void SuggestionsBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Escape)
            {
                HideSuggestionBox();
                e.Handled = true;
                return;
            }
        }

        private void SuggestionsBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SuggestionsBoxPopup.IsOpen == true)
            {
                HideSuggestionBox();
            }
        }
        #endregion
        #region[Supportive Function]
        private void ChangeAutoCompleteVisibility()
        {

            if (SuggestionsBoxPopup.IsOpen == true)
                HideSuggestionBox();
            else
                ShowSuggestionBox();
        }
        private void ShowSuggestionBox()
        {
            SuggestionsBoxPopup.IsOpen = true;
            Keyboard.Focus(SuggestionsBox);
            SuggestionsBox.LostFocus += SuggestionsBox_LostFocus;
        }
        private void HideSuggestionBox()
        {
            SuggestionsBoxPopup.IsOpen = false;
            SupportiveTextBox.Focus();
            SuggestionsBox.LostFocus -= SuggestionsBox_LostFocus;
        }
        #endregion


    }
}
