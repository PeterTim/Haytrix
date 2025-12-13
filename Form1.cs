namespace WinFormsAppMM
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
            InitializeWebViewAsync();

            webView21.Dock = DockStyle.Fill; // Fill the form

            webView21.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Application.Exit();
                }
            };
        }

        private void webView21_Click(object sender, EventArgs e)
        {
        }

        private async void InitializeWebViewAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);

            var html = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Assets",
                "matrix.html"
            );

            //var uri = new Uri(html).AbsoluteUri;

            //// Load a remote URL
            //webView21.Source = new Uri(html);

            // OR load a local HTML file
            // Add your index.html to the project:
            // - Right-click project → Add → Existing Item → select file
            // - Set Build Action: Content
            // - Set Copy to Output Directory: Copy if newer
            string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "matrix.html");
            webView21.Source = new Uri(localPath);

            // Optional: Handle full-screen video in page
            webView21.CoreWebView2.ContainsFullScreenElementChanged += (sender, args) =>
            {
                // Already full screen, but you can add extra logic if needed
            };
        }
    }
}