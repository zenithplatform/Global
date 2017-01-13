using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zenith.Client.Shared.ViewModel
{
    public class ErrorReportViewModel
    {
        [DllImport("kernel32.dll")]
        internal static extern uint GetTickCount();

        private readonly string ErrorDirectory = string.Format(@"{0}\Errors", Environment.CurrentDirectory);
        ExceptionViewModel _exception = null;
        SummaryViewModel _summary = null;
        string _messageText = "";

        public ErrorReportViewModel()
        {
            //_exception = new ExceptionViewModel();
            //_summary = new SummaryViewModel();
            _exception = MockException();
            _summary = MockSummary();
            _messageText = MockMessageText();
        }

        public ExceptionViewModel ExceptionDetails
        {
            get { return _exception; }
        }

        public SummaryViewModel Summary
        {
            get { return _summary; }
        }

        public string MessageText
        {
            get { return _messageText; }
        }

        //Only for test
        private SummaryViewModel MockSummary()
        {
            SummaryViewModel model = new SummaryViewModel();

            string strBuildTime = new DateTime(2000, 1, 1).AddDays(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build).ToShortDateString();

            // Gets program uptime
            TimeSpan timeSpanProcTime = Process.GetCurrentProcess().TotalProcessorTime;

            // Used to get disk space
            DriveInfo driveInfo = new DriveInfo(Directory.GetDirectoryRoot(System.Reflection.Assembly.GetExecutingAssembly().Location));

            model.AddSummaryItem("Current Date/Time", DateTime.Now.ToString());
            model.AddSummaryItem("Exec. Date/Time", Process.GetCurrentProcess().StartTime.ToString());
            model.AddSummaryItem("Build Date", strBuildTime);
            model.AddSummaryItem("OS", Environment.OSVersion.VersionString);
            model.AddSummaryItem("Language", "EN-US");
            model.AddSummaryItem("System Uptime", string.Format("{0} Days {1} Hours {2} Mins {3} Secs", Math.Round((decimal)GetTickCount() / 86400000), Math.Round((decimal)GetTickCount() / 3600000 % 24), Math.Round((decimal)GetTickCount() / 120000 % 60), Math.Round((decimal)GetTickCount() / 1000 % 60)));
            model.AddSummaryItem("Program Uptime", string.Format("{0} hours {1} mins {2} secs", timeSpanProcTime.TotalHours.ToString("0"), timeSpanProcTime.TotalMinutes.ToString("0"), timeSpanProcTime.TotalSeconds.ToString("0")));
            model.AddSummaryItem("PID", Process.GetCurrentProcess().Id.ToString());
            model.AddSummaryItem("Thread Count", Process.GetCurrentProcess().Threads.Count.ToString());
            model.AddSummaryItem("Thread Id", System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            model.AddSummaryItem("Executable", Assembly.GetExecutingAssembly().Location);
            model.AddSummaryItem("Process Name", Process.GetCurrentProcess().ProcessName);
            model.AddSummaryItem("Version", "0.10.0.0");
            model.AddSummaryItem("CLR Version", Environment.Version.ToString());

            return model;
        }

        private ExceptionViewModel MockException()
        {
            ExceptionViewModel model = new ExceptionViewModel();
            model.Exception = "at StackTrace.Program.GetValue() in C:\\DebuggingPresentation\\StackTrace\\StackTrace\\Program.cs:line 29\n" +
                              "at StackTrace.Program.Main(String[] args) in C:\\DebuggingPresentation\\StackTrace\\StackTrace\\Program.cs:line 15\n" +
                              "at System.AppDomain.nExecuteAssembly(Assembly assembly, String[] args)\n" +
                              "at System.AppDomain.ExecuteAssembly(String assemblyFile,\n" +
                              "Evidence assemblySecurity, String[] args)\n" +
                              "at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()\n" +
                              "at System.Threading.ThreadHelper.ThreadStart_Context(Object state)\n" +
                              "at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)\n" +
                              "at System.Threading.ThreadHelper.ThreadStart()";

            return model;
        }

        private string MockMessageText()
        {
            return "There was an unexpected error during application runtime.There was an unexpected error during application runtime.There was an unexpected error during application runtime.There was an unexpected error during application runtime.";
        }

        private bool SendErrorReport()
        {   
            string fileFormName = "uploadedfile";
            string contenttype = "application/octet-stream";
            string uploadfile = string.Format("{0}\\{1:yyyy}_{1:MM}_{1:dd}_{1:HH}{1:mm}{1:ss}.txt", ErrorDirectory, DateTime.Now);
            string errorReportURI = "http://www.bugreporturi.com/send.php";

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(errorReportURI);
            webrequest.CookieContainer = new CookieContainer();
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";


            // Build up the post message header

            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(Path.GetFileName(uploadfile));
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array
            // ensuring the boundary appears on a line by itself

            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            FileStream fileStream = new FileStream(uploadfile, FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            webrequest.ContentLength = length;

            try
            {
                Stream requestStream = webrequest.GetRequestStream();

                // Write out our post header
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                // Write out the file contents
                byte[] buffer = new Byte[checked((uint)Math.Min(4096,
                                         (int)fileStream.Length))];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    requestStream.Write(buffer, 0, bytesRead);

                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            }
            catch (WebException e)
            {
                //if (MessageBox.Show(this, e.Message, "Error", MessageBoxButton.YesNoCancel) == DialogResult.Retry)
                //    return SendErrorReport();
                //else
                //    return false;
            }

            // Write out the trailing boundary
            HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();

            return (response.StatusCode == HttpStatusCode.OK);
        }
    }

    public class NameValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ExceptionViewModel
    {
        private string _exceptionString = "";

        public string Exception
        {
            get { return _exceptionString; }
            set { _exceptionString = value; }
        }
    }

    public class SummaryViewModel
    {
        private ObservableCollection<NameValuePair> _summaryItems = null;

        public SummaryViewModel()
        {
            _summaryItems = new ObservableCollection<NameValuePair>();
        }

        public ObservableCollection<NameValuePair> SummaryItems
        {
            get { return _summaryItems; }
        }

        public void AddSummaryItem(string name, string value)
        {
            _summaryItems.Add(new NameValuePair() { Name = name, Value = value });
        }
    }
}
