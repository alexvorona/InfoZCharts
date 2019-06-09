using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using ChartWPF.Model;
using ChartWPF.View;

namespace ChartWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        const int sensorsCount = 3;

        private MasterControl _masterView;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _masterView = new MasterControl();
            CurrentView = _masterView;
            try
            {
                logger.Info($"Data Initialization for {sensorsCount} sensors");
                _graphsList = new ObservableCollection<Graph>();

                for (int i = 0; i < sensorsCount; i++)
                {
                    _graphsList.Add(new Graph(i, $"Sensor #{i + 1}", new Sensor()));
                }
                SeriesCollection = new SeriesCollection();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Data Initialization");
            }

        }               

        private string _title = "Master Form";
        public string FormTitle
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;                
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Graph> _graphsList;
        public ObservableCollection<Graph> GraphsList
        {
            get { return _graphsList; }
            set
            {
                _graphsList = value;
                RaisePropertyChanged();
            }
        }

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(() => { Application.Current.Shutdown(); });
                }
                return _exitCommand;
            }
        }

        private RelayCommand _viewMasterCommand;
        public RelayCommand ViewMasterCommand
        {
            get
            {
                if (_viewMasterCommand == null)
                {
                    _viewMasterCommand = new RelayCommand(() => 
                    {
                        SeriesCollection.Clear();
                        CurrentView = _masterView;
                        FormTitle = "Master Form";                        
                    });
                }
                return _viewMasterCommand;
            }
        }

        private RelayCommand<int> _viewDetailCommand;


        public RelayCommand<int> ViewDetailCommand
        {
            get
            {
                if (_viewDetailCommand == null)
                {                    
                    _viewDetailCommand = new RelayCommand<int>(obj =>
                    {
                        int id = (int)obj;
                        logger.Info($"Show Details for sensor id #{id}");
                        CurrentView = new DetailControl();
                        FormTitle = $"Details Form {_graphsList[id].Name}";
                        ShowGraph(id);
                    });
                }
                return _viewDetailCommand;
            }
        }

        private void ShowGraph(int id)
        {
            Graph graph = _graphsList[id];

            var series = new LineSeries
            {
                Title = graph.Name,
                Values = graph.Sensor.Values
            };
            var seriesCollection = new SeriesCollection();
            seriesCollection.Add(series);            
            SeriesCollection = seriesCollection;

        }
    }
}
