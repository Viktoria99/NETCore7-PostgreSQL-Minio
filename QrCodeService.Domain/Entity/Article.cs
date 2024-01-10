using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrCodeService.Domain.Entity
{
    /// <summary>
    /// Part of the transport document
    /// </summary>
    public class Article
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Part Id
        /// </summary>
        [Required]
        public int ArticleId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        /// <summary>
        /// The ID given by the operator to the file when
        /// uploading files
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string OperatorFileId { get; set; } = null!;
    }
}