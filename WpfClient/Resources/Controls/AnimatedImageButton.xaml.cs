using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient.Resources.Controls
{
    /// <summary>
    /// Lógica de interacción para AnimatedImageButton.xaml
    /// </summary>
    public partial class AnimatedImageButton : UserControl
    {
        public AnimatedImageButton()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(AnimatedImageButton), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(AnimatedImageButton), new PropertyMetadata(null));

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
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
