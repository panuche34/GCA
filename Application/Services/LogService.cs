using Application.Interface;
using ConstantManager.Exceptions;
using Core.Entity;
using Core.ViewModel;
using Infrastructure.Contexties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Util.Exceptions;

namespace Application.Services
{
    internal class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly AppDbContext _ctx;

        

        public LogService(ILogger<LogService> logger,
            AppDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
            
        }

        
        //public async Task<CompanyVM> CrudAsync(int id)
        //{
        //    var model = await _ctx.Logs
        //        .AsNoTracking()
        //        .Where(w => w.Id == id)
        //        .FirstOrDefaultAsync();
        //    if (model == null)
        //        return new LogVM();

        //    var vm = new LogVM
        //    {
        //        Id = model.Id,
        //        Name = model.Name,
        //        Cnpj = model.Cnpj,
        //        Emails = model.Emails,
        //        Remarks = model.Remarks,
        //        CertificateNotAfter = model.CertificateNotAfter ?? DateTime.MinValue,
        //        SerproClientId = model.SerproClientId ?? string.Empty,
        //        SerproClientSecret = model.SerproClientSecret ?? string.Empty,
        //    };
        //    return vm;
        //}

        //public async Task CrudAsync(LogVM viewModel)
        //{
        //    // Valida o certificado antes de salvar qualquer coisa            

        //    var model = await _ctx.Logs
        //        .Where(w => w.Id == (viewModel.Id.HasValue ? viewModel.Id.Value : 0))
        //        .FirstOrDefaultAsync();
        //    if (model == null)
        //    {
        //        model = new Log();
        //        model.CreatedOnUtc = DateTime.Now;
        //    }
        //    model.Name = viewModel.Name;
        //    model.Cnpj = viewModel.Cnpj;
        //    model.Emails = viewModel.Emails;
        //    model.Remarks = viewModel.Remarks;
        //    model.IsActive = true;
        //    model.IsDeleted = false;
        //    model.SerproClientId = viewModel.SerproClientId;
        //    model.SerproClientSecret = viewModel.SerproClientSecret;
        //    if (!string.IsNullOrWhiteSpace(viewModel.Password))
        //        model.CertificatePassword = viewModel.Password;

        //    if (certInfo is not null)
        //    {
        //        model.CertificateNotAfter = certInfo.NotAfter;
        //    }
        //    if (model.Id == 0)
        //    {
        //        await _ctx.AddAsync(model);
        //    }
        //    await _ctx.SaveChangesAsync();

        //    if (viewModel.CertificateFile is not null && viewModel.CertificateFile.Length > 0)
        //    {
        //        try
        //        {
        //            await _certificateService.SaveAsync(viewModel.CertificateFile,
        //                viewModel.Password, model.Id);

        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, $"Error saving certificate for company ID: {model.Id}");
        //            throw new CustomException($"Error saving certificate: {ex.Message}");
        //        }
        //    }
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var model = await _ctx.Companies
        //        .AsNoTracking()
        //        .Where(w => w.Id == id)
        //        .FirstOrDefaultAsync();
        //    if (model == null)
        //        throw new CustomException(ErrorMessagesConstant.EntityIdNotFound);

        //    model.IsActive = false;
        //    await _ctx.SaveChangesAsync();
        //}

        //public async Task ReactiveAsync(int id)
        //{
        //    var model = await _ctx.Companies
        //        .AsNoTracking()
        //        .Where(w => w.Id == id)
        //        .FirstOrDefaultAsync();
        //    if (model == null)
        //        throw new CustomException(ErrorMessagesConstant.EntityIdNotFound);

        //    model.IsActive = true;
        //    await _ctx.SaveChangesAsync();
        //}

        //public async Task<CertificateInfoVM> CertificateInfoAsync(int id)
        //{
        //    var company = await _ctx.Companies
        //        .Where(w => !w.IsDeleted && w.Id == id)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (company is null)
        //        throw new CustomException(ErrorMessagesConstant.EntityIdNotFound);

        //    var certInfo = await _certificateService
        //        .ValidateAsync(id, company.CertificatePassword ?? string.Empty);
        //    return certInfo;
        //}
    }
}
