using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Insuree;

namespace InsuranceV2.Application.Services
{
    public interface IInsureeManagementAppService
    {
        CreateOrEditInsuree GetExistingInsureeToEdit(int id);
        DetailInsuree GetDetailInsuree(int id);
        CreateOrEditInsuree GetNewInsuree();
        void CreateInsuree(CreateOrEditInsuree createInsuree);
        void EditInsuree(CreateOrEditInsuree editInsuree);
        void DeleteInsuree(int id);
        PagerModel<ListInsuree> GetPagedInsurees(int page = 1, int pageSize = 5, string sort = "Id", string sortDir = "ASC");
    }
}