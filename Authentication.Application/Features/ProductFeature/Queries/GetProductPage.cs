using Authenticaion.Domain.Entities;
using Authentication.Application.Common.Interfaces;
using Authentication.Application.Common.Pagination;
using Authentication.Application.Common.Response;
using Authentication.Application.Features.ProductFeature.ViewModels.Requests;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.ProductFeature.Queries
{
    public class GetProductPage : IRequest<ResponseVM>
    {
        public ProductRequest ProductRequest { get; set; }
    }

    public class GetContactPageQueryHandler : IRequestHandler<GetProductPage, ResponseVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetContactPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseVM> Handle(GetProductPage request, CancellationToken cancellationToken)
        {
            if (request.ProductRequest == null)
            {
                throw new ArgumentNullException();
            }
            else
            {

                var response = new ResponseVM();
                var products = await _unitOfWork.Repository<Product>()
                                                                       .GetAsync(page: request.ProductRequest.PageNumber == 0 ? 1 : request.ProductRequest.PageNumber,
                                                                       predicate:null,
                                                                       pageSize: request.ProductRequest.PageSize == 0 ? 5 : request.ProductRequest.PageSize,
                                                                       orderBy:null
                                                                       );

                response.Data = _mapper.Map<PagedResult<Product>>(products);

                return response;
            }


        }

        
    }



}