namespace Ecommerce.CustomValidations
{
    public static class ExtensionValidation
    {
        //Check Valid/Accepted Image Extensions
        public static bool IsImage(string Extension)
        {
            return Extension.ToLower() == "jpg" || Extension.ToLower() == "jpeg" ||
                   Extension.ToLower() == "png" || Extension.ToLower() == "bmp";
        }
    }
}
