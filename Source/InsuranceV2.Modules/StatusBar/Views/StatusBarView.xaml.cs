using System;
using System.Windows.Controls;

namespace InsuranceV2.Modules.StatusBar.Views
{
    /// <summary>
    /// Interaction logic for StatusBarView
    /// </summary>
    public partial class StatusBarView : UserControl
    {
        public StatusBarView()
        {
            InitializeComponent();

            Console.WriteLine("Add bindings for visibility to statusbar!");
        }
    }
}
