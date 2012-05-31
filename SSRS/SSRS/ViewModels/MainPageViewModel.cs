using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SSRS.Data;
using SSRS.Services;
using SSRS.Services.DTO;

namespace SSRS.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        ServiceClient<ReportExecutionRequest, ReportExecutionResponse> executionClient; 

        public MainPageViewModel()
        {
            IsBusy = true;

            Init();
            InitAndExecuteReportsService();
            InitAndExecuteReportExecutionService();
        }

        private void Init()
        {
            Reports = new ObservableCollection<ReportInfo>();
            Fields = new ObservableCollection<DynamicFormField>();
            SelectedExportFileTypes = ExportFileTypes[0];

            SubmitCommand = new RelayCommand(() => executionClient.Post("/reportExecution",
                new ReportExecutionRequest()
                    {
                        Format = SelectedExportFileTypes, 
                        Name = SelectedReport.Path,
                        Parameters = GetParameters()
                    }
            ));
        }

        private Parameter[] GetParameters()
        {
            var parameters = from f in Fields
                             select new Parameter() {Name = f.Caption, Value = f.Value};
            return parameters.ToArray();
        }

        private void InitAndExecuteReportsService()
        {
            var client = new ServiceClient<ReportsRequest, ReportsResponse>();
            client.Completed += (sender, args) =>
                {
                    // check for web exceptions
                    var webEx = args.Error as WebException;
                    if (webEx != null)
                    {
                        var webResponse = (HttpWebResponse) webEx.Response;

                        ErrorText = string.Format("WebException: {0} {1} {2}",
                            webResponse.ResponseUri,
                            webResponse.Method, webResponse.StatusDescription);

                        return;
                    }

                    // re-throw any other exceptions
                    if (args.Error != null)
                        throw args.Error;

                    var result = args.Response.Result;
                    if (result == null) return;

                    foreach (var reportInfo in result)
                    {
                        Reports.Add(reportInfo);
                    }

                    if (Reports.Count > 0)
                        SelectedReport = Reports[1];

                    IsBusy = false;
                };

            client.Post("/reports", new ReportsRequest() {IncludeParameters = true});
        }

        private void InitAndExecuteReportExecutionService()
        {
            executionClient = new ServiceClient<ReportExecutionRequest, ReportExecutionResponse>();
            executionClient.Completed += (sender, args) =>
            {
                // check for web exceptions
                var webEx = args.Error as WebException;
                if (webEx != null)
                {
                    var webResponse = (HttpWebResponse)webEx.Response;
                    ErrorText = webResponse.StatusDescription;
                    return;
                }

                // re-throw any other exceptions
                if (args.Error != null)
                    throw args.Error;

                var userSettings = IsolatedStorageSettings.ApplicationSettings;
                var filename = string.Concat(SelectedReport.Name, ".", SelectedExportFileTypes);
                userSettings.Add(filename, args.Response.Result);
            };
        }

        public string SelectedExportFileTypes { get; set; }

        readonly List<string> exportFileTypes = new List<string>(2) { "Excel", "PDF" }; 
        public List<string> ExportFileTypes
        {
            get { return exportFileTypes; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { get; set; }

        public ObservableCollection<ReportInfo> Reports { get; set; }

        private ReportInfo selectedReport;

        public ReportInfo SelectedReport
        {
            get { return selectedReport; }
            set
            {
                if (selectedReport == value) return;
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
                SetFields();
            }
        }

        private void SetFields()
        {
            Fields.Clear();
            foreach (var parameter in SelectedReport.Parameters)
            {
                Fields.Add(new DynamicFormField()
                               {
                                   Caption = parameter.Name,
                                   Nullable = parameter.Nullable,
                                   Type = parameter.ParameterType,
                                   Value = parameter.DefaultValues[0]
                               });
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set 
            { 
                if (isBusy == value) 
                return; isBusy = value; 
                OnPropertyChanged("IsBusy"); 
            }
        }

        public ObservableCollection<DynamicFormField> Fields { get; set; }
        
        private string errorText;
        public string ErrorText
        {
            get { return errorText; }
            private set
            {
                if (errorText == value) return; 
                errorText = value; 
                OnPropertyChanged("ErrorText");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            } 
        }
    }
}
