﻿using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class GeneralController<TModel, TViewModel> : ControllerBase
    {
        private readonly IGeneralRepository<TModel> _repository;
        private readonly IMapper<TModel, TViewModel> _mapper;
        private IGeneralRepository<Account> repository;
        private IMapper<Account, Account> mapper;

        public GeneralController(IGeneralRepository<TModel> repository, IMapper<TModel, TViewModel> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public GeneralController(IGeneralRepository<Account> repository, IMapper<Account, Account> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var models = _repository.GetAll();
            if (!models.Any())
            {
                return NotFound(new ResponseVM<TViewModel>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            var resultConverted = models.Select(_mapper.Map).ToList();

            return Ok(new ResponseVM<List<TViewModel>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = resultConverted
            });
        }

        [HttpGet("{guid}")]
        [AllowAnonymous]
        public IActionResult GetByGuid(Guid guid)
        {
            var model = _repository.GetByGuid(guid);
            if (model is null)
            {
                return NotFound(new ResponseVM<TViewModel>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            var resultConverted = _mapper.Map(model);

            return Ok(new ResponseVM<TViewModel>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = resultConverted
            });
        }

        [HttpPost]
        public IActionResult Create(TViewModel viewModel)
        {
            var model = _mapper.Map(viewModel);
            var result = _repository.Create(model);
            if (result is null)
            {
                return NotFound(new ResponseVM<TViewModel>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Create Failed"
                });
            }

            return Ok(new ResponseVM<TModel>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create Success",
                Data = result
            });
        }

        [HttpPut]
        public IActionResult Update(TViewModel viewModel)
        {
            var model = _mapper.Map(viewModel);
            var isUpdated = _repository.Update(model);
            if (!isUpdated)
            {
                return NotFound(new ResponseVM<TViewModel>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Update Failed"
                });
            }

            return Ok(new ResponseVM<TModel>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Success"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _repository.Delete(guid);
            if (!isDeleted)
            {
                return NotFound(new ResponseVM<TViewModel>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Delete Failed"
                });
            }

            return Ok(new ResponseVM<TModel>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Success"
            });
        }
    }
}
