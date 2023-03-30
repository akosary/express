namespace Ecommerce.CustomValidations
{
    public static class FileSizeValidation
    {
        public static bool IsValidSize(long fileSize, int allwedSizeMB)
            => fileSize > 0 && fileSize <= allwedSizeMB * 1051057;
    }
}
