using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using System.Threading.Tasks;
using PrimeMaths;
using System.Diagnostics;

namespace PrimeFinder
{
    public class MainWindowViewModel : BindableBase
    {
        private uint _nthPrimeToFind;
        private int _maxThreads;
        private bool _isProcessing;
        private ulong _nthPrimeResult;
        private TimeSpan _executionTime;

        public MainWindowViewModel()
        {
            this._nthPrimeToFind = 10000;
            this._maxThreads = 4;
            this.CalculateNthPrimeCommand = new DelegateCommand(this.CalculateNthPrime, this.CanCalculateNthPrime).ObservesProperty(() => this.IsProcessing);
        }

        public ICommand CalculateNthPrimeCommand { get; private set; }

        public bool IsProcessing
        {
            get => this._isProcessing;
            set => this.SetProperty(ref this._isProcessing, value);
        }

        public TimeSpan ExecutionTime
        {
            get => this._executionTime;
            set => this.SetProperty(ref this._executionTime, value);
        }

        public string NthPrimeToFind
        {
            get
            {
                return this._nthPrimeToFind.ToString();
            }

            set
            {
                if (uint.TryParse(value, out var result) && result > 0)
                {
                    this.SetProperty(ref this._nthPrimeToFind, result);
                }
            }
        }

        public ulong NthPrimeResult
        {
            get => this._nthPrimeResult;
            set => this.SetProperty(ref this._nthPrimeResult, value);
        }

        public int MaxThreads
        {
            get => this._maxThreads;
            set => this.SetProperty(ref this._maxThreads, value);
        }

        private bool CanCalculateNthPrime()
        {
            return !this.IsProcessing;
        }

        private async void CalculateNthPrime()
        {
            this.IsProcessing = true;

            var sw = Stopwatch.StartNew();

            this.NthPrimeResult = await Task.Run(() => Tools.FindNthPrimeParallel(this._nthPrimeToFind, this._maxThreads));
            this.ExecutionTime = sw.Elapsed;

            this.IsProcessing = false;
        }
    }
}
