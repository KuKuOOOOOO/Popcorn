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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Popcorn.Controls;

namespace Popcorn.UserControls.Home.Settings.ApplicationSettings
{
    /// <summary>
    /// Logique d'interaction pour ApplicationSettingsUserControl.xaml
    /// </summary>
    public partial class ApplicationSettingsUserControl
    {
        public ApplicationSettingsUserControl()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (AnimatedScrollViewer)sender;
            if (scv.TargetHorizontalOffset - e.Delta >= -Math.Abs(e.Delta) &&
                scv.TargetHorizontalOffset - e.Delta < scv.ScrollableWidth + Math.Abs(e.Delta))
            {
                scv.TargetHorizontalOffset -= e.Delta;
            }
        }
    }
}