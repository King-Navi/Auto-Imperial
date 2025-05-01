using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfClient.Resources.Controls
{
    /// <summary>
    /// Lógica de interacción para AnimatedLabelButton.xaml
    /// </summary>
    public partial class AnimatedLabelButton : UserControl
    {
        public AnimatedLabelButton()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(AnimatedLabelButton), new PropertyMetadata("Label"));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Brush), typeof(AnimatedLabelButton), new PropertyMetadata(Brushes.White));

        public Brush ForegroundColor
        {
            get { return (Brush)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double), typeof(AnimatedLabelButton), new PropertyMetadata(16.0));

        public double LabelFontSize
        {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(AnimatedLabelButton), new PropertyMetadata(null));

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var animation = (Storyboard)FindResource("ClickAnimation");
            animation.Begin(); 

            if (ClickCommand != null && ClickCommand.CanExecute(null))
            {
                ClickCommand.Execute(null);
            }
        }
    }
}
