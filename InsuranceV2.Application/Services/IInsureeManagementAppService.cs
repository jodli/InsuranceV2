using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Insuree;

namespace InsuranceV2.Application.Services
{
    public interface IInsureeManagementAppService
    {
        int PageSize { get; set; }
        PagerModel<DisplayInsuree> GetPagedInsurees(int page = 1, string sort = "Id", string sortDir = "ASC");
        CreateOrEditInsuree GetExistingInsureeToEdit(int id);
        DetailInsuree GetDetailInsuree(int id);
        CreateOrEditInsuree GetNewInsuree();
        void CreateInsuree(CreateOrEditInsuree createInsuree);
        void EditInsuree(CreateOrEditInsuree editInsuree);
        void DeleteInsuree(int id);
    }
}