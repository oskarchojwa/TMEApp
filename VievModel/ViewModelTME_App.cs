using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TME_App.VievModel
{
    using Model;
    using System.Windows.Input;

    public class ViewModelTME_App : INotifyPropertyChanged
    {
        private ModelTME_App model = new ModelTME_App();

        public string inputText //text wejściowy
        {
            get
            { 
                return model.inputText;
            }
            set
            {
                model.inputText = value; 
                onPropertyChanged(nameof(inputText));
            }
        }

        public double progressBarValue //wartość probressbara
        {
            get
            {
                return model.progressBarValue;
            }
        }

        public string quantityOfNumbersYouCanRandMessage //wiadomość przekazywana do widoku
        {
            get { return model.quantityOfNumbersYouCanRandMessage; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string nameOfProperty) //pozwala poinformować widok o zmianie wartości danej zmiennej aby ją zaktualizować w widoku
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameOfProperty));
            }
        }

        private ICommand inputTextToInt = null;

        public ICommand InputTextToInt //komenda drugiego przycisku po której następuje walidacja inputText a następnie losowanie liczb
        {
            get
            {
                if(inputTextToInt == null)
                {
                    inputTextToInt = new RelayCommand(
                        (object o) =>
                        {
                            model.inputTextToInt(inputText);
                            onPropertyChanged(nameof(progressBarValue)); //informuje widok o zmienie progress bar value, w przypadku gdy nie działa w moim programie asynchroniczna komenda tutaj aktualizuje się stałą wartością
                            onPropertyChanged(nameof(quantityOfNumbersYouCanRandMessage)); //informuje widok o zmianie wiadomosci którą wyświetla text block
                        },
                        (object o) =>
                        {
                            return model.progressBarValue >= 0; //warunek kiedy button będzie pozwalał się wcisnąć
                        });
                }
                return inputTextToInt;
            }
        }

        private ICommand loadDatabase = null;

        public ICommand LoadDatabase //komenda pierwszego przycisku pozwalająca na wczytanie danych z bazy
        {
            get
            {
                if (loadDatabase == null)
                {
                    loadDatabase = new RelayCommand(
                        (object o) =>
                        {
                            model.loadDatabase();
                            onPropertyChanged(nameof(quantityOfNumbersYouCanRandMessage));
                        },
                        (object o) =>
                        {
                            return model.progressBarValue >= 0; //warunek kiedy button będzie pozwalał się wcisnąć
                        });
                }
                return loadDatabase;
            }
        }

        //Poniżej znajdują się resztki kodu, w którym próbowałem w prosty sposób zaimplementować asynchroniczną komende aby aktualizował sie progress bar wraz ze zmieniającym się iteratorem pętli

        /*private ICommand inputTextToInt = null;

        public ICommand InputTextToIntCommand
        {
            get
            {
                inputTextToInt = new AsyncRelayCommand(async (object o) =>
                {
                    await countnumbers();
                });
                return inputTextToInt;
            }
        }
            


        public async Task countnumbers()
        {
            var progress = new Progress<double>(value =>
            {
                progressBarValue = value;
                Console.WriteLine(progressBarValue);
            });
            Console.WriteLine("TUTAJ");
            await Task.Run(() => LoopThroughNumbers(progress));
        }

        public void LoopThroughNumbers(IProgress<double> progress)
        {
            double zmienna = 0;
            for (int i = 0; i < 100; i++)
            {
                if (i % 1 == 0)
                {
                    Console.WriteLine(i);
                    zmienna += 1;
                    progress.Report(zmienna);
                }
            }
        }*/
    }
}
