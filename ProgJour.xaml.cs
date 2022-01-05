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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
