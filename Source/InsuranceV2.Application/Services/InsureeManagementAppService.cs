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
        private readonly ILogger<InsureeManagementAppService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public InsureeManagementAppService(IInsureeRepository insureeRepository, IUnitOfWorkFactory unitOfWorkFactory,
            ILogger<InsureeManagementAppService> logger, IMapper mapper)
        {
            _insureeRepository = insureeRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _logger = logger;
            _mapper = mapper;
        }

        public PagerModel<ListInsuree> GetPagedInsurees(int page = 1, int pageSize = 10, string sort = "Id",
            string sortDir = "ASC")
        {
            var totalRecords = _insureeRepository.FindAll().Count();
            var data = new List<ListInsuree>();
            var insurees =
                _insureeRepository.FindAll()
                    .OrderBy(BuildOrderBy(sort, sortDir))
                    .Skip(page*pageSize - pageSize)
                    .Take(pageSize);

            _mapper.Map(insurees, data);

            var model = new PagerModel<ListInsuree>
            {
                Data = data,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = (int) Math.Ceiling(totalRecords/(double) pageSize)
            };

            _logger.Info($"Returning PagerModel: {model}");

            return model;
        }

        public DetailInsuree GetDetailInsuree(int id)
        {
            var insuree = _insureeRepository.FindById(id, i => i.Addresses, i => i.PhoneNumbers, i => i.EmailAddresses,
                i => i.Partner, i => i.Insurances);
            var detailInsuree = new DetailInsuree();

            _mapper.Map(insuree, detailInsuree);

            return detailInsuree;
        }

        public AddOrEditInsuree GetNewInsuree()
        {
            var createOrEditInsuree = new AddOrEditInsuree();

            return createOrEditInsuree;
        }

        public AddOrEditInsuree GetExistingInsureeToEdit(int id)
        {
            var insuree = _insureeRepository.FindById(id);
            var createOrEditInsuree = new AddOrEditInsuree();

            _mapper.Map(insuree, createOrEditInsuree);

            return createOrEditInsuree;
        }

        public void AddInsuree(AddOrEditInsuree addInsuree)
        {
            try
            {
                using (_unitOfWorkFactory.Create())
                {
                    var insuree = new Insuree();
                    _mapper.Map(addInsuree, insuree);
                    _insureeRepository.Add(insuree);
                    _logger.Info($"Created insuree with id: {insuree.Id}");
                }
            }
            catch (ModelValidationException e)
            {
                _logger.Error("Could not create insuree.", e);
            }
        }

        public void EditInsuree(AddOrEditInsuree editInsuree)
        {
            try
            {
                using (_unitOfWorkFactory.Create())
                {
                    _logger.Info($"Update insuree with id: {editInsuree.Id}");
                    var insureeToUpdate = _insureeRepository.FindById(editInsuree.Id);
                    _mapper.Map(editInsuree, insureeToUpdate, typeof (AddOrEditInsuree), typeof (Insuree));
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
                _logger.Info($"Delete insuree with id: {id}");
                _insureeRepository.Remove(id);
            }
        }

        private static string BuildOrderBy(string sortOn, string sortDirection)
        {
            return sortOn.ToLower() == "fullname"
                ? $"FirstName {sortDirection}, LastName {sortDirection}"
                : $"{sortOn} {sortDirection}";
        }
    }
}