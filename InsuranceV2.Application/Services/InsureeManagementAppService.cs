using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using InsuranceV2.Application.Models;
using InsuranceV2.Application.Models.Insuree;
using InsuranceV2.Common.Exceptions;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Repositories;

namespace InsuranceV2.Application.Services
{
    public class InsureeManagementAppService : IInsureeManagementAppService
    {
        private readonly IInsureeRepository _insureeRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ILogger<InsureeManagementAppService> _logger;
        private readonly IMapper _mapper;

        public InsureeManagementAppService(IInsureeRepository insureeRepository, IUnitOfWorkFactory unitOfWorkFactory,
            ILogger<InsureeManagementAppService> logger, IMapper mapper)
        {
            _insureeRepository = insureeRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _logger = logger;
            _mapper = mapper;
        }

        public int PageSize { get; set; }

        public PagerModel<DisplayInsuree> GetPagedInsurees(int page = 1, string sort = "Id", string sortDir = "ASC")
        {
            int totalRecords = _insureeRepository.FindAll().Count();
            var data = new List<DisplayInsuree>();
            IQueryable<Insuree> insurees =
                _insureeRepository.FindAll()
                    .OrderBy(BuildOrderBy(sort, sortDir))
                    .Skip((page*PageSize) - PageSize)
                    .Take(PageSize);
            
            _mapper.Map(insurees, data);

            var model = new PagerModel<DisplayInsuree>
            {
                Data = data,
                PageNumber = page,
                PageSize = PageSize,
                TotalRows = totalRecords
            };

            return model;
        }

        private static string BuildOrderBy(string sortOn, string sortDirection)
        {
            return sortOn.ToLower() == "fullname"
                ? $"FirstName {sortDirection}, LastName {sortDirection}"
                : $"{sortOn} {sortDirection}";
        }

        public DetailInsuree GetDetailInsuree(int id)
        {
            var insuree = _insureeRepository.FindById(id);
            var detailInsuree = new DetailInsuree();

            _mapper.Map(insuree, detailInsuree);

            return detailInsuree;
        }

        public CreateOrEditInsuree GetNewInsuree()
        {
            var createOrEditInsuree = new CreateOrEditInsuree();

            return createOrEditInsuree;
        }

        public CreateOrEditInsuree GetExistingInsureeToEdit(int id)
        {
            var insuree = _insureeRepository.FindById(id);
            var createOrEditInsuree = new CreateOrEditInsuree();

            _mapper.Map(insuree, createOrEditInsuree);

            return createOrEditInsuree;
        }

        public void CreateInsuree(CreateOrEditInsuree createInsuree)
        {
            try
            {
                using (_unitOfWorkFactory.Create())
                {
                    var insuree = new Insuree();
                    _mapper.Map(createInsuree, insuree);
                    _insureeRepository.Add(insuree);
                    _logger.Info($"Created insuree with id: {insuree.Id}");
                }
            }
            catch (ModelValidationException e)
            {
                _logger.Error("Could not create insuree.", e);
            }
        }

        public void EditInsuree(CreateOrEditInsuree editInsuree)
        {
            try
            {
                using (_unitOfWorkFactory.Create())
                {
                    var insureeToUpdate = _insureeRepository.FindById(editInsuree.Id);
                    _mapper.Map(editInsuree, insureeToUpdate, typeof (CreateOrEditInsuree), typeof (Insuree));
                    _logger.Info($"Edited insuree with id: {insureeToUpdate.Id}");
                }
            }
            catch (ModelValidationException e)
            {
                _logger.Error("Could not update insuree.", e);
            }
        }

        public void DeleteInsuree(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                _insureeRepository.Remove(id);
                _logger.Info($"Deleted insuree with id: {id}");
            }
        }
    }
}