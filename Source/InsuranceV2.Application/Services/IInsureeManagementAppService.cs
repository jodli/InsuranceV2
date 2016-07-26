using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Insuree;

namespace InsuranceV2.Application.Services
{
    public interface IInsureeManagementAppService
    {
        AddOrEditInsuree GetExistingInsureeToEdit(int id);
        DetailInsuree GetDetailInsuree(int id);
        AddOrEditInsuree GetNewInsuree();
        void AddInsuree(AddOrEditInsuree addInsuree);
        void EditInsuree(AddOrEditInsuree editInsuree);
        void DeleteInsuree(int id);
        PagerModel<ListInsuree> GetPagedInsurees(int page = 1, int pageSize = 5, string sort = "Id", string sortDir = "ASC");
    }
}