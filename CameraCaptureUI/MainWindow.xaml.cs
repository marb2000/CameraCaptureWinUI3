
using System;

using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;

using System.Numerics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.System;
using Windows.UI;
using Windows.UI.Composition;


namespace CameraCaptureUI
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            var captureUI = new Windows.Media.Capture.CameraCaptureUI();

            // CameraCaptureUI doesn't implement Win32 interop pattern so it's not possivle to use it in WinUI3.
            // as alternative, use the Windows.Media.Capture.MediaCapture APIs 
            
            //var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            //WinRT.Interop.InitializeWithWindow.Initialize(captureUI, hWnd);

            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
        }
    }
}

