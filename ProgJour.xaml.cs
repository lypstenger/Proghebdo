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
        public string Jour
        {
            get

            {
                return (string) LbJour.Content;
            }
            set
            {
                LbJour.Content = value;
            }
        }
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
            int k = (int)(GdJour.Width / 96);
            GdJour.Width = k * 96;
            Coeffquartheure = GdJour.Width / 96;

            double pasTemp = 50;//11 Skmarge.ActualHeight / 6;
            double pasTime = (GdJour.Width / 12);
             for (int x = 0; x < 6; x++)
            {
                LsligneTemp[x].Y1 = LsligneTemp[x].Y2 = (pasTemp * (x + 1))+10;
                LsEtiquettesTemp[x].Margin = new Thickness(0, (pasTemp * (x + 1) +(10-6)), 5, 0);
                LsEtiquettesTemp[x].Content = 25 - (x * 5);
            }
            for (int x = 0; x < LsligneHeure.Count; x++)
            {
                LsligneHeure[x].X1 = LsligneHeure[x].X2 = pasTime * (x);
                LsligneHeure[x].Y1 = pasTemp * (0 + 1)+10;
                LsligneHeure[x].Y2 =250 + LsligneHeure[x].Y1;
            }

            foreach (epingle ep in GdJour.Children.OfType<epingle>().ToList())
            {
                ep.Max = 250 ;
            }

        }
        Point posjoy;
        Point pointcurrent;
        Point pointpas;

        public event Action<double[], double> position;

        private void Epingle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (current == null) { return; }
            GdJour.Children.Remove(current);
            GdJour.Children.Add(current);
            posjoy = e.GetPosition(current);
            pointcurrent = e.GetPosition(GdJour);
            pointpas = e.GetPosition(GdJour);
 
        }
        public List<HH_Etat> Mesetats()
        {
            List<HH_Etat> l = new List<HH_Etat>();
            foreach (epingle ep in (from ep in GdJour.Children.OfType<epingle>()
                                    orderby ep.Deplace ascending
                                    select ep).ToList())
            {
                l.Add(new HH_Etat(ep.Heure, ep.Température));
            }
            

            return l;
        }

        internal void Update(PlanningJour planningJour)
        {
            GdJour.Children.Clear();
            foreach(HH_Etat he in planningJour.List_HH_Etat)
            {
                epingle tpepingle = DefEpingle();
                tpepingle.Deplace = calcul_pos(he.Temps);
                tpepingle.DeplaceTemp.Y = calcul_poscursor(he.Etat);
                tpepingle.Heure = he.Temps;
                tpepingle.Température = he.Etat;

              GdJour.Children.Add(tpepingle);

            }
        }

        private int  calcul_poscursor(int etat)
        {
            double coeffTemp = 250 / 25;
            int res =(int) (250 - Math.Round(etat * coeffTemp));


            return res;
        }

        private int calcul_pos(string h)
        {
            int hh = Convert.ToInt32(h.Split(new string[] { ":" }, StringSplitOptions.None)[0]);
            int Min =(int) (Convert.ToInt32(h.Split(new string[] { ":" }, StringSplitOptions.None)[1])/15);
            int position = (int)(((hh * 4)+Min) * Coeffquartheure);
            return position;
        }
        private epingle DefEpingle()
        {
            epingle tmp = new epingle
            {
                Width = 44,
                Margin = new Thickness(-22, 01, 0, 0),
                Max = 250,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            tmp.MouseLeftButtonDown += Epingle_MouseLeftButtonDown_1;
            tmp.MouseRightButtonUp += Tmp_MouseRightButtonUp;
            tmp.SuppEpingle += Tmp_SuppEpingle; ;
            return tmp;
        }

        private void Epingle_MouseMove(object sender, MouseEventArgs e)
        {
            if (current == null) { return; }
            if (current.current != null) { return; }

            if (e.LeftButton == MouseButtonState.Pressed == true)
            {
                pointcurrent = e.GetPosition(GdJour);
                pointcurrent.X = pointcurrent.X + (posjoy.X - 22);


                int k = (int)Math.Round(((pointcurrent.X - posjoy.X) + 22) / Coeffquartheure);
                current.deplace.X = k * Coeffquartheure;
                if (current.deplace.X < 0) { current.deplace.X = 0; }

                if (current.deplace.X > GdJour.Width)
                {
                    current.deplace.X = GdJour.Width;
                }

                current.Heure = convertHeure(current.deplace.X);


                //intpas = (int)(pointcurrent.X / Coeffquartheure);

            }

        }
        private void Epingle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (current != null)
            {
                current.current = null;
            }
            current = null;

        }
        private string convertHeure(double pos)
        {
            double nbqh = pos / Coeffquartheure;
            int h = (int)(nbqh / 4);
            nbqh = nbqh - (4 * h);

            return h.ToString("00") + ":" + (nbqh * 15).ToString("00");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            epingle tmp = new epingle
            {
                 Width = 44,
                Margin = new Thickness(-22, 01, 0, 0),
                Max =250,
                 HorizontalAlignment = HorizontalAlignment.Left,
            };
            tmp.MouseLeftButtonDown += Epingle_MouseLeftButtonDown_1;
            tmp.MouseRightButtonUp += Tmp_MouseRightButtonUp;
            tmp.SuppEpingle += Tmp_SuppEpingle; ;
            GdJour.Children.Add(tmp);

        }

        private void Tmp_SuppEpingle(epingle obj)
        {
            GdJour.Children.Remove(obj);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UserControl_SizeChanged(null, null);
        }
    }
}
