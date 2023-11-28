namespace GameZone.Attributes
{
    public class AllowedExtensionsAttributes: ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensionsAttributes(string allowedExtensions) 
        {
            _allowedExtensions = allowedExtensions; 
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null) 
            {
            var extension =Path.GetExtension(file.FileName);
                var isAllowed =_allowedExtensions.Split(separator:',').Contains(extension,StringComparer.OrdinalIgnoreCase);
                if (!isAllowed )
                {
                    return new ValidationResult(errorMessage: $"Only {_allowedExtensions} are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
