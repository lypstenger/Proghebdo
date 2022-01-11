using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        //public bool Active { get; set; } = false;

        public Grid current = null;
        public epingle()
        {
            InitializeComponent();
        }
        Point posjoy;
        Point pointcurrent;
        double MovX_Val = 0;
        double MovY_Val = 0;

        Point pointpas;
        public event Action<epingle> SuppEpingle;
        public int coeffTemp;

        private void Epingle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Active = true;
            posjoy = e.GetPosition(Gdpostemp);
            pointcurrent = e.GetPosition(gdTemp);
            pointpas = e.GetPosition(gdTemp);
            current = Gdpostemp;
            coeffTemp = (int)Math.Round(Max / 25);
        }

        private void Epingle_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed == true && current != null)
            {
                pointcurrent = e.GetPosition(gdTemp);
                pointcurrent.Y = (pointcurrent.Y - 30) + (-posjoy.Y);

                if (pointcurrent.Y < -10)
                {
                    pointcurrent.Y = -10;
                }


                if (pointcurrent.Y > 240)
                {
                    pointcurrent.Y = 240;
                }


                int k = (int)Math.Round(pointcurrent.Y / coeffTemp);
                //DeplaceTemp.Y = k * coeffTemp;

                DeplaceTemp.Y = (pointcurrent.Y+10);
                LbTemp.Content = convertpostoTemp(DeplaceTemp.Y);


                return;

            }
            current = null;
        }
        private string convertpostoTemp(double d)
        {

            string res = (25 - Math.Round(d / coeffTemp)).ToString("00");

            return res;
        }
        private void Epingle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            current = null;
        }

        private void LbTemp_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void LbTemp_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void gdTemp_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            coeffTemp = (int)Math.Round(Max / 25);
            coeffTemp = 10;
            int k = (int)(e.Delta / 100);

            DeplaceTemp.Y = DeplaceTemp.Y - (k * coeffTemp);


            //DeplaceTemp.Y = DeplaceTemp.Y + 12;
            if (DeplaceTemp.Y < 0)
            {
                DeplaceTemp.Y = 0;
            }


            if (DeplaceTemp.Y > Max)
            {
                DeplaceTemp.Y = Max;
            }

            LbTemp.Content = convertpostoTemp(DeplaceTemp.Y);
            Point relativePoint = ((Grid)(sender)).PointToScreen(new Point(0, 0));
            SetPosition((int)relativePoint.X + 22, (int)relativePoint.Y + 10);
        }
        private void SetPosition(int a, int b)
        {
            SetCursorPos(a, b);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private void Btsupp_Click(object sender, RoutedEventArgs e)
        {
            if (SuppEpingle != null)
            {
                SuppEpingle(this);
            }
        }
    }
}
