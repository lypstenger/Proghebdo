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
    /// Logique d'interaction pour ProgJour.xaml
    /// </summary>
    public partial class ProgJour : UserControl
    {
        private List<Line> LsligneTemp = new List<Line>();
        private List<Line> LsligneHeure = new List<Line>();
        private List<Label> LsEtiquettesTemp = new List<Label>();
        public ProgJour()
        {
            InitializeComponent();
            LsligneTemp = Gdecran.Children.OfType<Line>().Where(li => (string)li.Tag == "H").ToList();
            LsligneHeure = Gdecran.Children.OfType<Line>().Where(li => (string)li.Tag == "V").ToList();
            LsEtiquettesTemp = Gdmarge.Children.OfType<Label>().ToList();


        }

        private double Coeffquartheure;
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            GdJour.Width = Gdecran.ActualWidth - 60;
            Coeffquartheure = GdJour.Width / 96;
            int k =(int) (GdJour.Width / 96);
            GdJour.Width = k * 96;
            if (position != null )
            {
           //if (current == null) { return; }
                 double[] ds = new double[] {
                     (double)k,Coeffquartheure
                    };

                position(ds, GdJour.ActualWidth);
            }


            double pasTemp = Skmarge.ActualHeight /6;
            double pasTime = (GdJour.Width / 12);
            GdJour.Margin = new Thickness(0, (pasTemp), 0, 0);
            for (int x = 0; x < 6; x++)
            {
                LsligneTemp[x].Y1 = LsligneTemp[x].Y2 = pasTemp * (x + 1);
                LsEtiquettesTemp[x].Margin = new Thickness(0, (pasTemp * (x + 1) - 6), 5, 0);
                LsEtiquettesTemp[x].Content = 25 - (x * 5);
            }
            for (int x = 0; x < LsligneHeure.Count; x++)
            {
                LsligneHeure[x].X1 = LsligneHeure[x].X2 = pasTime * (x );
                LsligneHeure[x].Y1 = GdJour.Margin.Top;
                LsligneHeure[x].Y2 = Skmarge.ActualHeight;
            }
   

        }
        Point posjoy;
        Point pointcurrent;
          double MovX_Val = 0;
            double MovY_Val = 0;

        Point pointpas;
        private void Epingle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (current == null) { return; }
             posjoy = e.GetPosition(current);
            pointcurrent = e.GetPosition(GdJour);
            pointpas = e.GetPosition(GdJour);
            if (position != null)
            {
                double[] ds = new double[] {
                        current.deplace.X,
                        pointcurrent.X,
                    posjoy.X,
                    (pointcurrent.X - pointpas.X),
                    };

                position(ds, GdJour.ActualWidth);
            }


        }
        public event Action<double[], double> position;


        private void Epingle_MouseMove(object sender, MouseEventArgs e)
        {
            if (current == null) { return; }
            if (e.LeftButton == MouseButtonState.Pressed == true)
            {
                pointcurrent = e.GetPosition(GdJour);



                //if (Math.Abs(pointcurrent.X -pointpas.X) < Coeffquartheure) { return; }



                if (pointcurrent.X < 0) { pointcurrent.X = 0; }

                if (pointcurrent.X > GdJour.Width) {
                    pointcurrent.X = GdJour.Width; 
                }



                int intpas =(int)( pointcurrent.X / Coeffquartheure);
                //pointcurrent.X = intpas ;
                pointpas.X = pointcurrent.X;
                current.deplace.X = (pointcurrent.X - posjoy.X) +22;

                current.Heure = pointcurrent.X.ToString("00,000");

                 if (position != null)
                {
                    double[] ds = new double[] { 
                        current.deplace.X, 
                        GdJour.ActualWidth,
                    Coeffquartheure,
                    (pointcurrent.X - pointpas.X),
                    posjoy.X,
                    (double)intpas
                    };

                    position(ds, GdJour.ActualWidth);
                }

            }

        }

        private void Epingle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            current = null;
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            epingle tmp= new epingle
            {
                Height = 428,
                Width=44,
                Margin = new Thickness(-22, -30, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            tmp.MouseLeftButtonDown += Epingle_MouseLeftButtonDown_1;
            tmp.MouseRightButtonUp += Tmp_MouseRightButtonUp;
            GdJour.Children.Add(tmp);
           
        }

        private void Tmp_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            GdJour.Children.Remove((epingle)sender);
        }

        epingle current = null;
        private void Epingle_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            current = (epingle)(sender);
        }
    }
}
