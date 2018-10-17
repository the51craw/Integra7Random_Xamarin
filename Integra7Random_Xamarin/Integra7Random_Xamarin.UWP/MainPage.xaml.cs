﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Integra7Random_Xamarin.UWP;
using Xamarin.Forms;
using Integra7Random_Xamarin;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(GenericHandlerInterface))]

[assembly: Dependency(typeof(MIDI))]

namespace Integra7Random_Xamarin.UWP
{
    public class GenericHandlerInterface : IGenericHandler
    {
        public MainPage mainPage { get; set; }

        public void GenericHandler(object sender, object e)
        {
            if (mainPage.midi.midiOutPort == null)
            {
                mainPage.midi.Init("INTEGRA-7");
            }
        }
    }

    public sealed partial class MainPage
    {
        // For accessing Integra7Random_Xamarin.MainPage from UWP:
        public Integra7Random_Xamarin.MainPage MainPage_Portable { get; set; }
        public Integra7Random_Xamarin.UWP.MainPage MainPage_UWP { get; set; }

        // Invisible comboboxes used by MIDI class (will always have INTEGRA-7 selected):
        private Picker OutputSelector;
        private Picker InputSelector;
        public MIDI midi;

        // For accessing the genericHandlerInterface:
        GenericHandlerInterface genericHandlerInterface;
        public Windows.UI.Core.CoreDispatcher Dispatcher_UWP { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Integra7Random_Xamarin.App());
            Init();
        }
        
        private async Task Init()
        {
            // Get dispatcher:
            Dispatcher_UWP = Dispatcher;

            // Get INTEGRA7Random.MainPage:
            MainPage_Portable = Integra7Random_Xamarin.MainPage.GetMainPage();
            UIHandler.appType = UIHandler._appType.UWP;

            // Get the generic handler (same way as done in Integra7Random_Xamarin.UIHandler):
            genericHandlerInterface = (Integra7Random_Xamarin.UWP.GenericHandlerInterface)DependencyService.Get<IGenericHandler>();

            // Let genericHandlerInterface know this MainPage:
            genericHandlerInterface.mainPage = this;

            // Let portable know this MainPage:
            MainPage_Portable.MainPage_Device = this;

            // Draw UI (function is in mainPage.uIHandler):
            //await Task.Run(() => MainPage_Portable.uIHandler.ShowLibrarianPage());
            MainPage_Portable.uIHandler.ShowLibrarianPage();

            // We need invisible ComboBoxes to hold settings from the
            // corresponding Pickers in the Xamarin code.
            OutputSelector = MainPage_Portable.uIHandler.Librarian_midiOutputDevice;
            InputSelector = MainPage_Portable.uIHandler.Librarian_midiInputDevice;

            MainPage_Portable.SetDeviceSpecificMainPage(this);

            MainPage_Portable.uIHandler.commonState.midi.Init(MainPage_Portable, "INTEGRA-7", OutputSelector, InputSelector, (object)Dispatcher_UWP, 0, 0);

            // Create a MyFileIO object:
            //MyFileIO fileIO = new MyFileIO();
            //MainPage_Portable.uIHandler.myFileIO.SetMainPagePortable(MainPage_Portable);

            // Always start by showing librarian:
            MainPage_Portable.uIHandler.ShowLibrarianPage();
        }

        public Windows.UI.Core.CoreDispatcher GetDispatcher()
        {
            return Dispatcher_UWP;
        }
    }
}
