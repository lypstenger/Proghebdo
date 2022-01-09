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

        public string Heure
        {
            get
            {
                return (string)LbHeure.Content;
            }
            set
            {
                LbHeure.Content = value;
            }
        }
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
        public double Max { get; set; }
        public epingle()
        {
            InitializeComponent();
        }
        Point posjoy;
        Point pointcurrent;
        double MovX_Val = 0;
        double MovY_Val = 0;

        Point pointpas;
        public event Action<double[], double> position;

        Grid current = null;
        private void Epingle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            posjoy = e.GetPosition(Gdpostemp);
            pointcurrent = e.GetPosition(gdTemp);
            pointpas = e.GetPosition(gdTemp);
            current = Gdpostemp;
        }

        private void Epingle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed == true && current!=null)
            {
               pointcurrent = e.GetPosition(gdTemp);
               pointcurrent.Y =(pointcurrent.Y-27) + (-posjoy.Y );

                if (pointcurrent.Y < 0)
                {
                    pointcurrent.Y = 0;
                }


                if (pointcurrent.Y >Max)
                {
                    pointcurrent.Y = Max;
                }



                DeplaceTemp.Y = (pointcurrent.Y );
                LbTemp.Content = DeplaceTemp.Y;
   

            }

        }
        private void Epingle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            current = null;

        }

    }
}
