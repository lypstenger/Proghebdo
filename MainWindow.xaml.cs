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

namespace Proghebdo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Label> lesdbgs = new List<Label>();
        public MainWindow()
        {
            InitializeComponent();
            //programmateurJour.position += ProgrammateurJour_position;
            lesdbgs = sk1.Children.OfType<Label>().ToList();

        }

        private void ProgrammateurJour_position(double[] arg1, double arg2)
        {

            for (int x = 0; x < arg1.Length; x++)
            {
                lesdbgs[x].Content = arg1[x].ToString("0.000");
            }
           
        }
    }
}
