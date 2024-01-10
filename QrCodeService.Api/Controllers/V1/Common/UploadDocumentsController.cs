using Microsoft.AspNetCore.Mvc;
using QrCodeService.Domain.Model;
using QrCodeService.Rabbit.Publisher.Services;
using QrCodeService.Rabbit.Types.Tasks;

namespace QrCodeService.Api.Controllers.V1.Common
{
    [ApiController]
    [Route("v{version:apiVersion}/common/[controller]")]
    [ApiVersion("1")]
    public class UploadDocumentsController : ControllerBase
    {
        private readonly IPartnerServicePublisher<DocumentRabbitTask> _publishDocument;
        private readonly IPartnerServicePublisher<QrCodeRabbitTask> _publishQrCode;
        public UploadDocumentsController(IPartnerServicePublisher<DocumentRabbitTask> publishService, IPartnerServicePublisher<QrCodeRabbitTask> publishQrCode)
        {
            _publishDocument = publishService;
            _publishQrCode = publishQrCode;
        }
        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Response(StatusCodes.Status200OK)]
        [Response(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        [Response(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadDocument([FromBody] TrafficDocument request)
        {

            var message = new DocumentRabbitTask
            {
                Data = request
            };
            await _publishDocument.PublishRequestAsync(message);
            return Ok();
        }
        /// <summary>
        /// Send request to receive Id.
        /// </summary>
        /// <param name="transportInvoideId"></param>
        /// <returns></returns>
        [HttpGet("{transportInvoideId}/GetId")]
        [Response(StatusCodes.Status200OK)]
        [Response(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetId(int transportInvoideId)
        {
            await Task.Delay(1000);
            return Ok();
        }
        /// <summary>
        /// Send request to get QR-code.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}/GenerateQrCode")]
        [Response(StatusCodes.Status200OK)]
        [Response(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQrCode([FromBody] QrCode qrCode)
        {
            var message = new QrCodeRabbitTask
            {
                Data = qrCode
            };
            await _publishQrCode.PublishRequestAsync(message);
            return Ok();

        }
    }
}