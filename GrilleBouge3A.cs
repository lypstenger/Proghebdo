using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace W3A
{
    public class GrilleBouge3A : ContentControl
    {
        Storyboard sbdOuvrir;
        Storyboard sbdFermer;

        /// <summary>
        /// Récupère les storyboard du template
        /// </summary>
        public override void OnApplyTemplate()
        {
            Grid leGrid = GetTemplateChild("leGrid") as Grid; //récupère le grid du Template
            sbdOuvrir = leGrid.Resources["sbdOuvrir"] as Storyboard;//puis les storyboard
            sbdFermer = leGrid.Resources["sbdFermer"] as Storyboard;
        }

        public static readonly DependencyProperty TitreProperty = DependencyProperty.Register
            (
                "Titre",
                typeof(string),
                typeof(GrilleBouge3A)
            );

        public string Titre
        {
            get { return (string)GetValue(TitreProperty); }
            set { SetValue(TitreProperty, value); }
        }

        public static readonly DependencyProperty HauteurProperty = DependencyProperty.Register
            (
                "Hauteur",
                typeof(double),
                typeof(GrilleBouge3A),
                new PropertyMetadata(250.0, OnHauteurChange)
            );

        public double Hauteur
        {
            get
            {
                return (double)GetValue(HauteurProperty); ;
            }
            set
            { 
                SetValue(HauteurProperty, value);
            }
        }


        private static void OnHauteurChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("OnHauteurChange");
            Console.WriteLine(((GrilleBouge3A)d).Hauteur);
        }

        /// <summary>
        /// Inverse l'état du contrôle ouvert <=> fermé
        /// </summary>
        public void InverseEtat()
        {
            Console.WriteLine("Height {0}", ActualHeight);
            if (ActualHeight == Hauteur)
                sbdFermer.Begin();
            else
                sbdOuvrir.Begin();
        }
        public void ouvre(bool c)
        {
   
            if (!c)
                sbdFermer.Begin();
            else
                sbdOuvrir.Begin();
        }
    }
}
