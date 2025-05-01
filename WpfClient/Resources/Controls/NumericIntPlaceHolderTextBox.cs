using System.Windows.Input;
using System.Windows;

namespace WpfClient.Resources.Controls
{
    public class NumericIntPlaceHolderTextBox : SimplePlaceHolderTextBox
    {
        static NumericIntPlaceHolderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericIntPlaceHolderTextBox),
                new FrameworkPropertyMetadata(typeof(NumericIntPlaceHolderTextBox)));
        }

        public NumericIntPlaceHolderTextBox()
        {
            DataObject.AddPastingHandler(this, OnPaste); 
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
            base.OnPreviewTextInput(e);
        }

        private static bool IsTextAllowed(string text)
        {
            return int.TryParse(text, out _); 
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = e.DataObject.GetData(DataFormats.Text) as string;
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
