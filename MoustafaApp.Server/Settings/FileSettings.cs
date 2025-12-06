namespace moustafapp.Server.Settings
{
    public static class FileSettings
    {

        public const string ImagePath = "/assets/images/Products";
        public const string AllowedPhotoExtintion = ".jpg, .png, .jpeg";
        public const int MaxAllowedPhotoSizeInMB = 1;
        //MB to Bytes Converter موقع تحويل الميجا لبايت 
        public const int MaxAllowedPhotoSizeInByte = MaxAllowedPhotoSizeInMB * 1024 * 1024;
    }
}
 