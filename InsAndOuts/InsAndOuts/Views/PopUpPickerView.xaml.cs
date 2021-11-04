using InsAndOuts.Services;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPickerView
    {
        public PopUpPickerView()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        private void Type1RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type2RadioButton.IsChecked = newCheckedState;
                Type3RadioButton.IsChecked = newCheckedState;
                Type4RadioButton.IsChecked = newCheckedState;
                Type5RadioButton.IsChecked = newCheckedState;
                Type6RadioButton.IsChecked = newCheckedState;
                Type7RadioButton.IsChecked = newCheckedState;

                PageCommunication.Instance.IntegerValue = 1;
                PageCommunication.Instance.StringValue  = Type1DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }

        private void Type2RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type1RadioButton.IsChecked = newCheckedState;
                Type3RadioButton.IsChecked = newCheckedState;
                Type4RadioButton.IsChecked = newCheckedState;
                Type5RadioButton.IsChecked = newCheckedState;
                Type6RadioButton.IsChecked = newCheckedState;
                Type7RadioButton.IsChecked = newCheckedState;
                
                PageCommunication.Instance.IntegerValue = 2;
                PageCommunication.Instance.StringValue  = Type2DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }

        private void Type3RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type1RadioButton.IsChecked = newCheckedState;
                Type2RadioButton.IsChecked = newCheckedState;
                Type4RadioButton.IsChecked = newCheckedState;
                Type5RadioButton.IsChecked = newCheckedState;
                Type6RadioButton.IsChecked = newCheckedState;
                Type7RadioButton.IsChecked = newCheckedState;
                
                PageCommunication.Instance.IntegerValue = 3;
                PageCommunication.Instance.StringValue  = Type3DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }

        private void Type4RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type1RadioButton.IsChecked = newCheckedState;
                Type2RadioButton.IsChecked = newCheckedState;
                Type3RadioButton.IsChecked = newCheckedState;
                Type5RadioButton.IsChecked = newCheckedState;
                Type6RadioButton.IsChecked = newCheckedState;
                Type7RadioButton.IsChecked = newCheckedState;
                
                PageCommunication.Instance.IntegerValue = 4;
                PageCommunication.Instance.StringValue = Type4DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }

        private void Type5RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type1RadioButton.IsChecked = newCheckedState;
                Type2RadioButton.IsChecked = newCheckedState;
                Type3RadioButton.IsChecked = newCheckedState;
                Type4RadioButton.IsChecked = newCheckedState;
                Type6RadioButton.IsChecked = newCheckedState;
                Type7RadioButton.IsChecked = newCheckedState;

                PageCommunication.Instance.IntegerValue = 5;
                PageCommunication.Instance.StringValue  = Type5DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }

        private void Type6RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type1RadioButton.IsChecked = newCheckedState;
                Type2RadioButton.IsChecked = newCheckedState;
                Type3RadioButton.IsChecked = newCheckedState;
                Type4RadioButton.IsChecked = newCheckedState;
                Type5RadioButton.IsChecked = newCheckedState;
                Type7RadioButton.IsChecked = newCheckedState;
                
                PageCommunication.Instance.IntegerValue = 6;
                PageCommunication.Instance.StringValue  = Type6DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }

        private void Type7RadioButton_OnStateChanged(object                sender
                                                   , StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
                
                var newCheckedState = ! e.IsChecked.Value;

                Type1RadioButton.IsChecked = newCheckedState;
                Type2RadioButton.IsChecked = newCheckedState;
                Type3RadioButton.IsChecked = newCheckedState;
                Type4RadioButton.IsChecked = newCheckedState;
                Type5RadioButton.IsChecked = newCheckedState;
                Type6RadioButton.IsChecked = newCheckedState;
                
                PageCommunication.Instance.IntegerValue = 7;
                PageCommunication.Instance.StringValue  = Type7DescriptionLabel.Text;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                ((SfRadioButton)sender).IsChecked = e.IsChecked.Value;
            }
        }
    }
}