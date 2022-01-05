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
    /// Logique d'interaction pour epingle.xaml
    /// </summary>
    public partial class epingle : UserControl
    {
        public double Deplace
        {
            get
            {
                return deplace.X;
            }
            set
            {
                deplace.X = value;
            }
        }
        public epingle()
        {
            InitializeComponent();
        }
      }
}
