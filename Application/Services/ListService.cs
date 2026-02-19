using Application.Interface;
using Core.Enumerators;
using Core.ViewModel;
using Core.VO;
using Infrastructure.Contexties;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    internal class ListService: IListService
    {
        private readonly AppDbContext _ctx;

        public ListService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        #region autocomplete
        
        //public async Task<IEnumerable<AutoCompleteVO>> AutocompleteAsync(
        //    TypeTagHelperEntity type, string term, UserVM userLogged, 
        //    int? idToFilter = null)
        //{
        //    var loweredTerm = term?.ToLower() ?? "";
        //    var intTerm = Int64.MinValue.TryParse(term);

        //    switch (type)
        //    {
        //        case TypeTagHelperEntity.Users:
        //            return await _ctx.Users
        //                .AsNoTracking()
        //                .Where(w => w.IsActive && !w.IsDeleted &&
        //                    (EF.Functions.ILike(w.UserName, $"%{loweredTerm}%") 
        //                        || w.Id == intTerm))
        //                .Select(s => new AutoCompleteVO(s.Id, s.UserName))
        //         .ToListAsync();

        //        case TypeTagHelperEntity.Company:
        //            return await _ctx.Companies
        //                .AsNoTracking()
        //                .Where(w => w.IsActive && !w.IsDeleted &&
        //                    (EF.Functions.ILike(w.Name, $"%{loweredTerm}%")
        //                        || EF.Functions.ILike(w.Cnpj, $"%{loweredTerm}%")
        //                        || w.Id == intTerm))
        //                .Select(s => new AutoCompleteVO(s.Id, s.Name))
        //         .ToListAsync();
        //        // Adicione outros casos conforme necessário
        //        default:
        //            throw new ArgumentException($"Unsupported autocomplete type: {type}");
        //    }
        //}

        #endregion autocomplete

        #region input_select

        //public async Task<IEnumerable<AutoCompleteVO>> GetSelectAsync(TypeTagHelperEntity type)
        //{
        //    switch (type)
        //    {
        //        case TypeTagHelperEntity.Users:
        //            return await _ctx.Users
        //               .AsNoTracking()
        //               .Where(w => w.IsActive && !w.IsDeleted)
        //               .Select(s => new AutoCompleteVO { Id = s.Id.ToString(), Term = s.UserName })
        //                .OrderBy(o => o.Term)
        //               .ToListAsync();

        //        // Adicione outros casos conforme necessário
        //        case TypeTagHelperEntity.Company:
        //        default:
        //            throw new ArgumentException($"Unsupported autocomplete type: {type}");
        //    }
        //}

        #endregion input_select

        #region datatable

        //public async Task<DataTableResult<SearchDatatableVM>> SearchForDataTableAsync(
        //    DataTableAdvancedVM<SearchDatatableFilterVM> filter, UserVM userLogged)
        //{
        //    switch (filter.Filters.SearchType)
        //    {
        //        case TypeTagHelperEntity.Users:
        //            return await DataTableUserAsync(filter);

        //        case TypeTagHelperEntity.Company:
        //            return await DataTableCompanyAsync(filter);

        //        // Adicione outros casos conforme necessário
        //        default:
        //            throw new ArgumentException($"Unsupported autocomplete type: {filter.Filters.SearchType}");
        //    }
        //}

        //private async Task<DataTableResult<SearchDatatableVM>> DataTableUserAsync(
        //    DataTableAdvancedVM<SearchDatatableFilterVM> filter)
        //{
        //    var query = _ctx.Users
        //        .Where(w => w.IsActive && !w.IsDeleted)
        //        .AsQueryable();
        //    if (!string.IsNullOrEmpty(filter.search.value))
        //    {
        //        query = query
        //            .Where(w => EF.Functions.ILike(w.UserName, $"%{filter.search.value}%"));
        //    }

        //    var order = filter.order[0];
        //    switch (order.column)
        //    {
        //        case 0:
        //            query = query.OrderByProperty(s => s.Id, order.IsAsc);
        //            break;
        //        case 1:
        //            query = query.OrderByProperty(s => s.UserName, order.IsAsc);
        //            break;
        //        default:
        //            query = query.OrderByProperty(s => s.Id, order.IsAsc);
        //            break;
        //    }

        //    var totalRecords = await query.CountAsync();

        //    var datas = await query
        //        .Select(s => new SearchDatatableVM { Id = s.Id.ToString(), Description = s.UserName })
        //        .Skip(filter.Start)
        //        .Take(filter.Length)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return new DataTableResult<SearchDatatableVM>
        //    {
        //        Data = datas,
        //        TotalRecords = totalRecords,
        //        TotalRecordsFiltered = datas.Count,
        //    };
        //}

        //private async Task<DataTableResult<SearchDatatableVM>> DataTableCompanyAsync(
        //    DataTableAdvancedVM<SearchDatatableFilterVM> filter)
        //{
        //    var query = _ctx.Companies
        //        .Where(w => w.IsActive && !w.IsDeleted)
        //        .AsQueryable();
        //    if (!string.IsNullOrEmpty(filter.search.value))
        //    {
        //        query = query
        //            .Where(w => EF.Functions.ILike(w.Name, $"%{filter.search.value}%"));
        //    }

        //    var order = filter.order[0];
        //    switch (order.column)
        //    {
        //        case 0:
        //            query = query.OrderByProperty(s => s.Id, order.IsAsc);
        //            break;
        //        case 1:
        //            query = query.OrderByProperty(s => s.Name, order.IsAsc);
        //            break;
        //        default:
        //            query = query.OrderByProperty(s => s.Id, order.IsAsc);
        //            break;
        //    }

        //    var totalRecords = await query.CountAsync();

        //    var datas = await query
        //        .Select(s => new SearchDatatableVM { Id = s.Id.ToString(), Description = s.Name })
        //        .Skip(filter.Start)
        //        .Take(filter.Length)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return new DataTableResult<SearchDatatableVM>
        //    {
        //        Data = datas,
        //        TotalRecords = totalRecords,
        //        TotalRecordsFiltered = datas.Count,
        //    };
        //}

        #endregion datatable
    }
}
