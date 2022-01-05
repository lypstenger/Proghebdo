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

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GdJour.Width = Gdecran.ActualWidth - 60;
            double pasTemp = Skmarge.ActualHeight /6;
            double pasTime = GdJour.Width / (LsligneHeure.Count-1);
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
                //LsEtiquettesTemp[x].Margin = new Thickness(0, (pasTemp * (x + 1) - 6), 5, 0);
                //LsEtiquettesTemp[x].Content = 25 - (x * 5);
            }

        }
        Point posjoy;
        Point pointcurrent;
          double MovX_Val = 0;
            double MovY_Val = 0;

        private void Epingle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (current == null) { return; }
             posjoy = e.GetPosition(current);
            pointcurrent = e.GetPosition(GdJour);
  

        }

        private void Epingle_MouseMove(object sender, MouseEventArgs e)
        {
            if (current == null) { return; }
            if (e.LeftButton == MouseButtonState.Pressed == true)
            {
                pointcurrent = e.GetPosition(GdJour);
                current.deplace.X = pointcurrent.X - posjoy.X;
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
                Margin = new Thickness(0, 10, 0, 0),
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
