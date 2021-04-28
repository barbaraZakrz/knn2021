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
using System.Globalization;

namespace knn2021
{

    public partial class MainWindow : Window
    {
        public class Wiersz
        {
            public List<double> parametry;
            public int klasaD;

            public Wiersz(List<double> parametryIn, int klasaDIn)
            {
                parametry = parametryIn;
                klasaD = klasaDIn;
            }
        }

        class Program
        {
            public static List<Wiersz> importData(string fileName)
            {
                string wiersz;
                string[] wierszSplit;
                List<Wiersz> allData = new List<Wiersz>();
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                while (file.ReadLine() != null)
                {
                    List<double> parametry = new List<double>();
                    wiersz = file.ReadLine();
                    wierszSplit = wiersz.Split(' ');
                    for (int i = 0; i < (wierszSplit.Length - 1); i++)
                    {
                        parametry.Add(double.Parse(wierszSplit[i], CultureInfo.InvariantCulture));
                    }
                    Wiersz Obiekt = new Wiersz(parametry, int.Parse(wierszSplit[wierszSplit.Length-1]));
                    allData.Add(Obiekt);
                }
                file.Close();
                return allData;

            }

            public static void wyswietl(List<Wiersz> Obiekty)
            {
                for (int i = 0; i < Obiekty.Count; i++)
                {
                    for (int j = 0; j < Obiekty[i].parametry.Count; j++)
                    {
                        Console.WriteLine(Obiekty[i].parametry[j]);
                    }

                }

            }


            public static List<Wiersz> normalizacja(List<Wiersz> lista)
            {
                List<double> min = new List<double>(lista[0].parametry);
                List<double> max = new List<double>(lista[0].parametry);

                for (int i = 1; i < lista.Count; i++)
                {
                    for (int j = 0; j < min.Count; j++)
                    {
                        if (lista[i].parametry[j] < min[j])
                        {
                            min[j] = lista[i].parametry[j];
                        }

                        if (lista[i].parametry[j] > max[j])
                        {
                            max[j] = lista[i].parametry[j];
                        }
                    }
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    for (int j = 0; j < min.Count; j++)
                    {
                        lista[i].parametry[j] = (lista[i].parametry[j] - min[j]) / (max[j] - min[j]);
                    }
                }

                return lista;
            }

            public static double euklides(Wiersz obiekt1, Wiersz obiekt2)
            {
                double suma = 0;
                for (int i = 0; i< obiekt1.parametry.Count; i++)
                {
                    suma += Math.Pow(obiekt1.parametry[i] - obiekt2.parametry[i], 2);
                }
                return Math.Sqrt(suma);
            }

            public static double manhattan(Wiersz obiekt1, Wiersz obiekt2)
            {
                double suma = 0;
                for (int i =0; i< obiekt1.parametry.Count; i++)
                {
                    suma += Math.Abs(obiekt1.parametry[i] - obiekt2.parametry[i]);
                }
                return suma;
            }

            public static double logarytm(Wiersz obiekt1, Wiersz obiekt2)
            {
                double suma = 0;
                for (int i =0; i<obiekt1.parametry.Count; i++)
                {
                    suma += Math.Abs(Math.Log(obiekt1.parametry[i]) - Math.Log(obiekt2.parametry[i]));
                }
                return suma;
            }

            public static double czebyszew(Wiersz obiekt1, Wiersz obiekt2)
            {
                double suma = 0;
                for (int i=0; i< obiekt1.parametry.Count; i++)
                {
                   if( suma < Math.Abs(obiekt1.parametry[i] - obiekt2.parametry[i]))
                    {
                        suma = Math.Abs(obiekt1.parametry[i] - obiekt2.parametry[i]); 
                    }
                
                }

                return suma;
            }

            public static double minkowski(Wiersz obiekt1, Wiersz obiekt2, double p =2)
            {
                double suma = 0;
                for (int i =0; i<obiekt1.parametry.Count; i++)
                {
                    suma = Math.Pow(Math.Pow(Math.Abs(obiekt1.parametry[i] - obiekt2.parametry[i]), p), 1 / p);
                }
                return suma; 
            }
           
            public MainWindow()
            {
                InitializeComponent();
            }


        }
    }
}
