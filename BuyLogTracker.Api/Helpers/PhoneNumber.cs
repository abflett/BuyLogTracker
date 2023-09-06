namespace BuyLogTracker.Api.Helpers
{
    public static class PhoneNumber
    {
        public static string FormatPhoneNumberForDisplay(string phoneNumber)
        {
            // Remove all non-digit characters from the phone number
            string cleanedPhoneNumber = new(phoneNumber.Where(char.IsDigit).ToArray());

            // Define the mask format and initialize an empty formatted number
            string maskFormat = "X (XXX) XXX - XXXX";
            string formattedPhoneNumber = string.Empty;

            // Start from the end of the mask and iterate backward through the digits
            int maskIndex = maskFormat.Length - 1;
            for (int i = cleanedPhoneNumber.Length - 1; i >= 0; i--)
            {
                char digit = cleanedPhoneNumber[i];

                // Replace the rightmost 'X' in the mask with the current digit
                while (maskIndex >= 0 && maskFormat[maskIndex] != 'X')
                {
                    formattedPhoneNumber = maskFormat[maskIndex] + formattedPhoneNumber;
                    maskIndex--;
                }

                // Add the current digit to the formatted number
                formattedPhoneNumber = digit + formattedPhoneNumber;

                // Move to the previous character in the mask
                maskIndex--;
            }

            // Add any remaining characters from the mask
            while (maskIndex >= 0)
            {
                formattedPhoneNumber = maskFormat[maskIndex] + formattedPhoneNumber;
                maskIndex--;
            }

            return formattedPhoneNumber;
        }

        public static string FormatPhoneNumberForStorage(string phoneNumber)
        {
            return new string(phoneNumber.Where(char.IsDigit).ToArray());
        }
    }
}
