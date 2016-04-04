﻿using AkkaMvvm.Interfaces;
using AkkaMvvm.Views;
using System;
using System.Windows;

namespace AkkaMvvm.App
{
    public partial class App : Application
    {
        #region Constants

        private const int retryCount = 1;
        private const int retryInterval = 2;

        #endregion Constants

        #region Fields

        private MainWindow _mainWindow;

        #endregion Fields

        public App()
        {
            Startup += App_Startup;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // let's just display a message and carry on regardless... yee haa! :D

            var window = _mainWindow;

            if (window == null || window.Visibility == Visibility.Hidden)
            {
                MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
                MessageBox.Show("Exception occurred before window available. Exiting application");
                Current.Shutdown();
            }
            else
            {
                MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
            }

            e.Handled = true;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var mainWindowViewModel = MEF.GetInstance<IMainWindowViewModel>();

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = mainWindowViewModel;
            _mainWindow.Show();
        }
    }
}
