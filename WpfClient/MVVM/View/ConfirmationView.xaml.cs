﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.MVVM.View
{
    public partial class ConfirmationView : Window
    {
        public ConfirmationView()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                if (DataContext is ConfirmationViewModel vm)
                {
                    vm.CloseRequested += result =>
                    {
                        Close();
                    };
                }
            };
        }
    }
}
