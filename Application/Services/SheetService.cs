using Application.Interface;
using ConstantManager.SerproApi;
using Core.Entity;
using Core.SerproApi;
using Infrastructure.Contexties;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Util.Exceptions;
using Util.Extensions;


namespace Application.Services
{
    internal class SheetService : ISheetService
    {
        private readonly ILogger<SheetService> _logger;
        private readonly AppDbContext _ctx;
        private SerproAuthResponse _serproAuthResponse;
        private readonly string _pathBaseOrig;
        private readonly string _pathBaseResult;

        public SerproService(ILogger<SheetService> logger, AppDbContext ctx,
            IConfiguration configuration)
        {
            _logger = logger;
            _ctx = ctx;
            _pathBaseOrig = configuration["PathBaseOrig"] ?? string.Empty;
            _pathBaseResult = configuration["PathBaseResult"] ?? string.Empty;
        }

       

    
        public async Task GetSheetAsync()
        {
            if (string.IsNullOrWhiteSpace(_pathBaseResult))
                throw new CustomException("path to result not config in appsettings.json");
            if (string.IsNullOrWhiteSpace(_pathBaseOrig))
                throw new CustomException("path to source not config in appsettings.json");


            //aqui a magina acontece

           
        }

       
    }
}