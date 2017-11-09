namespace ImageCompare.lib
{
    interface IImageCompare
    {
        void CompareImages(string bmp1, string bmp2, byte threshold = 3);
        void CropImage(string bmp1, int x, int y, int width, int height);

    }
}
