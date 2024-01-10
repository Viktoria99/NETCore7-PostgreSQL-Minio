namespace QrCodeService.Domain.Types
{
    public enum EventStatus
    {

        Success = 1,
        /// <summary>
        /// Problems with sending to the HUB.
        /// </summary>
        Failed = 2,
        /// <summary>
        /// No approvement from the HUB.
        /// </summary>
        UnApprove = 3
    }
}
