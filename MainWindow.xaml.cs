using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

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
           mysto = (Storyboard)this.FindResource("OnMOUVE");

        }

        private void ProgrammateurJour_position(double[] arg1, double arg2)
        {

            for (int x = 0; x < arg1.Length; x++)
            {
                lesdbgs[x].Content = arg1[x].ToString("0.000");
            }

        }
        Storyboard mysto = null;

        private void RadioButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double valeur = Convert.ToDouble(((RadioButton)(sender)).Tag);
            ThicknessAnimationUsingKeyFrames d = (ThicknessAnimationUsingKeyFrames)mysto.Children[0];
            EasingThicknessKeyFrame es = (EasingThicknessKeyFrame)d.KeyFrames[0];
            es.Value = new Thickness(0, valeur, 0, 0);
            mysto.Begin(this);
        }
        DirectoryInfo dir = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            skrdBs.Children.OfType<RadioButton>().Where(rb => (string)rb.Tag == "0").ToList()[0].IsChecked = true;
            Listfile.ItemsSource = dir.GetFiles("*.*").Where(s => s.Extension == ".xhml").ToList();


        }
        List<PlanningJour> EnrPlaningjour = new List<PlanningJour>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnrPlaningjour = new List<PlanningJour>();
            foreach (ProgJour Pj in skJours.Children)
            {
                PlanningJour Paj = new PlanningJour(Pj.Jour, Pj.Mesetats());

                EnrPlaningjour.Add(Paj);
            }
            Saveconf();

        }

        private void Saveconf()
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<PlanningJour>));
            StreamWriter reader = new StreamWriter(TbFilexml.Text);
            mySerializer.Serialize(reader, (EnrPlaningjour));
            reader.Close();
            Listfile.ItemsSource = dir.GetFiles("*.*").Where(s => s.Extension == ".xhml").ToList();

        }
        private void Loadconf()
        {

            if (TbFilexml.Text.Trim() == "")
            {
                TbFilexml.Text = "default.xhml";
                Saveconf();
            }
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<PlanningJour>));
            StreamReader reader = new StreamReader(TbFilexml.Text);
            EnrPlaningjour = (List<PlanningJour>)mySerializer.Deserialize(reader);
            reader.Close();
        }

        private void Listfile_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((FileInfo)(Listfile.SelectedItem) == null) { return; }
            TbFilexml.Text = ((FileInfo)(Listfile.SelectedItem)).Name;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Loadconf();
            foreach (ProgJour Pj in skJours.Children)
            {
                List<PlanningJour> p = (from pla in EnrPlaningjour
                                        where pla.NomJour == Pj.Jour
                                        select pla).ToList();

                if (p.Count > 0)
                {
                    Pj.Update(p[0]);
                }

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string strtout = "";
            //EnrPlaningjour = new List<PlanningJour>();
            foreach (ProgJour Pj in skJours.Children)
            {
                PlanningJour Paj = new PlanningJour(Pj.Jour, Pj.Mesetats());
                strtout = strtout + Paj.NomJour + ";";
                foreach (HH_Etat item in Paj.List_HH_Etat)
                {
                    strtout = strtout  + item.Temps + ";" + item.Etat + ";";
                }

                strtout = strtout + "\r";

                //EnrPlaningjour.Add(Paj);
            }
               TkFiletxt.Text = strtout;

        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
             this.WindowState = WindowState.Minimized;
        }

        private void Quit_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
         if (this.WindowState == WindowState.Maximized)
            {
                //       this.WindowState = WindowState.Normal;
            }
            else
            {
                //         this.WindowState = WindowState.Maximized;
            }

        }
 
  
        private void Titre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

  

    }

    public class HH_Etat
        {

            public string Temps { get; set; }
            public int Etat { get; set; }
            public HH_Etat() { }
            public HH_Etat(string h, int t)
            {
                Temps = h;
                Etat = t;
            }



        }
        public class PlanningJour
        {

            public List<HH_Etat> List_HH_Etat { get; set; } = new List<HH_Etat>();
            public string NomJour { get; set; }
            public PlanningJour() { }
            public PlanningJour(string nj, List<HH_Etat> lsj)
            {
                List_HH_Etat.Clear();
                List_HH_Etat.AddRange(lsj);
                NomJour = nj;
            }


        }

    }

