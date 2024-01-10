
namespace QrCodeService.Domain.Types
{
    public enum MessageStatus
    {
        /// <summary>
        /// Sent for processing
        /// </summary>
        Send = 0,

        HandleSuccess = 1,

        HandleError = 2,

        Unknown = 3
    }
}