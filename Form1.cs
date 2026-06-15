using Microsoft.Web.WebView2.Core;

namespace MatrixScreenSaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;     // No border/title bar
            WindowState = FormWindowState.Maximized;    // Maximized (covers taskbar on most setups)
            TopMost = true;                             // Stay on top

            // For true kiosk (cover taskbar completely)
            Bounds = Screen.PrimaryScreen.Bounds;

            // Initialize WebView2

            webView21.Dock = DockStyle.Fill; // Fill the form

            InitializeWebViewAsync();
        }

        private async void InitializeWebViewAsync()
        {
            var userDataFolder = Path.Combine(Path.GetTempPath(), "MatrixScreenSaverWebView2");
            var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
            await webView21.EnsureCoreWebView2Async(env);

            // Capture keyboard events from WebView2 to exit on any key
            webView21.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = true;
            webView21.CoreWebView2.Settings.IsScriptEnabled = true;

            // Inject script to exit on any keyboard or (significant) mouse activity.
            await webView21.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(@"
                (function () {
                    var origin = null;
                    function exit() { window.chrome.webview.postMessage('exit'); }
                    document.addEventListener('keydown', exit, true);
                    document.addEventListener('mousedown', exit, true);
                    document.addEventListener('wheel', exit, true);
                    document.addEventListener('mousemove', function (e) {
                        if (origin === null) { origin = { x: e.screenX, y: e.screenY }; return; }
                        if (Math.abs(e.screenX - origin.x) > 10 || Math.abs(e.screenY - origin.y) > 10) {
                            exit();
                        }
                    }, true);
                })();
            ");

            // Listen for messages from WebView2
            webView21.CoreWebView2.WebMessageReceived += (sender, args) =>
            {
                if (args.TryGetWebMessageAsString() == "exit")
                {
                    Application.Exit();
                }
            };

            // Read HTML from embedded resource
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = "MatrixScreenSaver.assets.matrix.html";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var htmlContent = await reader.ReadToEndAsync();
                        webView21.NavigateToString(htmlContent);
                    }
                }
            }

            // Optional: Handle full-screen video in page
            webView21.CoreWebView2.ContainsFullScreenElementChanged += (sender, args) =>
            {
                // Already full screen, but you can add extra logic if needed
            };
        }
    }
}