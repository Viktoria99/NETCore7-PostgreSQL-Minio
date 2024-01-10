using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrCodeService.Domain.Entity
{
    public class Message
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string? ArticleType { get; set; }

        public int RouteId { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        [Required]
        public DateTimeOffset Updated { get; set; }
        /// <summary>
        /// The ID given by the operator to the sent message.
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string OperatorMessageId { get; set; } = null!;
        /// <summary>
        ///<paramref name="MessageStatus">
        /// </summary>
        [Required]
        public int Status { get; set; }
        /// <summary>
        /// Request body
        /// </summary>
        [Required]
        public string? Data { get; set; }
        /// <summary>
        /// The result of executing the request.
        /// Contains a response or error.
        /// </summary>
        public string? Result { get; set; }
    }
}