﻿using AvilesaBusManagementSystem.ViewModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvilesaBusManagementSystem.Features.Itinerario
{
    /// <summary>
    /// Lógica de interacción para ItinerariosView.xaml
    /// </summary>
    public partial class ItinerarioView : Page
    {
        public ItinerarioView(ItinerarioViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

        }
    }
}
