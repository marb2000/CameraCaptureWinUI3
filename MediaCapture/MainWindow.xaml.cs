
using System;
using System.Threading.Tasks;

using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;

namespace MediaCapture
{

    public sealed partial class MainWindow : Window
    {
        Windows.Media.Capture.MediaCapture _mediaCapture;


        public MainWindow()
        {
            this.InitializeComponent();
        }
        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (_mediaCapture == null)
            {
                _mediaCapture = new Windows.Media.Capture.MediaCapture();
                await _mediaCapture.InitializeAsync();
            }

            await TakePhoto();

            //await SavePhoto(imgFormat);
        }

        private async Task TakePhoto()
        {
            var encodingProperties = ImageEncodingProperties.CreateJpeg();
            var bitmap = new WriteableBitmap((int)imageViewer.Width, (int)imageViewer.Height);
            using (var imageStream = new InMemoryRandomAccessStream())
            {
                // Take photo
                await _mediaCapture.CapturePhotoToStreamAsync(encodingProperties, imageStream);
                await imageStream.FlushAsync();
                imageStream.Seek(0);
                bitmap.SetSource(imageStream);
                imageViewer.Source = bitmap;
            }
        }

        private async Task TakePhotoAndSave()
        {
            var encodingProperties = ImageEncodingProperties.CreateJpeg();

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                "TestPhoto.jpg",
                CreationCollisionOption.GenerateUniqueName);

            // Take photo
            await _mediaCapture.CapturePhotoToStorageFileAsync(encodingProperties, file);

            BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));

            // imagePreview is a <Image> object defined in XAML
            imageViewer.Source = bmpImage;
        }
    }
}
