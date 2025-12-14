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

            // Exit on any key press (Form level)
            this.KeyPreview = true;
            this.KeyDown += (s, e) => Application.Exit();

            // Exit on any key press (WebView2 level)
            webView21.PreviewKeyDown += webView21_PreviewKeyDown;

            InitializeWebViewAsync();
        }

        private async void InitializeWebViewAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);

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

        private void webView21_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Application.Exit();
        }
    }
}