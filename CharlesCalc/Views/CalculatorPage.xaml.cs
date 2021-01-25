using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CharlesCalc.Views
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
            lblTitle.FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)) * 3;
        }
    }
}
