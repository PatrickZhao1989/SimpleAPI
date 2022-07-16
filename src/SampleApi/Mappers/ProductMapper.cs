using AutoMapper;
using SampleApi.Entities;
using SampleApi.Models;

namespace SampleApi.Mappers;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
	}
}