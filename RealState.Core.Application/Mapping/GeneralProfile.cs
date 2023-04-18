using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Application.ViewModels.Users;
using RealState.Core.Domain.Entities;
using RealState.Core.Application.ViewModels.Favourite;
using RealState.Core.Application.Features.Sales_.Commands.UpdateCategory;
using RealState.Core.Application.Features.Upgrates.Commands.UpdateCategory;
using RealState.Core.Application.Features.Upgrates.Commands.CreateUpgrates;
using RealState.Core.Application.Features.PropertyType.Commands.UpdateCategory;
using RealState.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealState.Core.Application.Features.Sales_.Commands.CreateSales;
using RealState.Core.Application.Features.Sales_.Commands.UpdateSales;

namespace RealState.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<RegisterRequest, SaveAdminViewModel>()
             .ForMember(x => x.HasError, opt => opt.Ignore())
             .ForMember(x => x.Error, opt => opt.Ignore())
             .ReverseMap();

            CreateMap<RegisterRequest, EditUserViewModel>()
           .ForMember(x => x.HasError, opt => opt.Ignore())
           .ForMember(x => x.Error, opt => opt.Ignore())
           .ReverseMap();


            CreateMap<Ads, SavePropertyViewModel>()
              .ReverseMap()
              .ForMember(x => x.FavouriteProperties, opt => opt.Ignore())
              .ForMember(x => x.CreatedDate, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore());


            CreateMap<Ads, PropertyViewModel>()
              .ReverseMap()
              .ForMember(x => x.FavouriteProperties, opt => opt.Ignore())
              .ForMember(x => x.CreatedDate, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore());


            CreateMap<FavouriteProperties, SaveFavouriteViewModel>()
             .ReverseMap()
             .ForMember(x => x.Ads, opt => opt.Ignore())
             .ForMember(x => x.CreatedDate, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<FavouriteProperties, FavouriteViewModel>()
            .ReverseMap()
            .ForMember(x => x.Ads, opt => opt.Ignore())
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore());


            CreateMap<AdsType, SavePropertyTypeViewModel>()
            .ReverseMap()
            .ForMember(x => x.Ads, opt => opt.Ignore())
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsUpgrates, SaveUpgradeViewModel>()
             .ReverseMap()
             .ForMember(x => x.Ads, opt => opt.Ignore())
             .ForMember(x => x.CreatedDate, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<Sales, SaveSalesViewModel>()
           .ReverseMap()
           .ForMember(x => x.Ads, opt => opt.Ignore())
           .ForMember(x => x.CreatedDate, opt => opt.Ignore())
           .ForMember(x => x.CreatedBy, opt => opt.Ignore())
           .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
           .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<Sales, MantSalesViewModel>()
        .ReverseMap()
        .ForMember(x => x.Ads, opt => opt.Ignore())
        .ForMember(x => x.CreatedDate, opt => opt.Ignore())
        .ForMember(x => x.CreatedBy, opt => opt.Ignore())
        .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
        .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsType, MantPropertyViewModel>()
        .ReverseMap()
        .ForMember(x => x.Ads, opt => opt.Ignore())
        .ForMember(x => x.CreatedDate, opt => opt.Ignore())
        .ForMember(x => x.CreatedBy, opt => opt.Ignore())
        .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
        .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsUpgrates, MantUpgradesViewModel>()
        .ReverseMap()
        .ForMember(x => x.Ads, opt => opt.Ignore())
        .ForMember(x => x.CreatedDate, opt => opt.Ignore())
        .ForMember(x => x.CreatedBy, opt => opt.Ignore())
        .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
        .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsType, CreatePropertyTypeCommand>()
        .ReverseMap()
        .ForMember(x => x.Ads, opt => opt.Ignore())
        .ForMember(x => x.CreatedDate, opt => opt.Ignore())
        .ForMember(x => x.CreatedBy, opt => opt.Ignore())
        .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
        .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsType, PropertyTypeUpdateResponse>()
      .ReverseMap()
      .ForMember(x => x.Ads, opt => opt.Ignore())
      .ForMember(x => x.CreatedDate, opt => opt.Ignore())
      .ForMember(x => x.CreatedBy, opt => opt.Ignore())
      .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
      .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<Sales, CreateSalesCommand>()
    .ReverseMap()
    .ForMember(x => x.Ads, opt => opt.Ignore())
    .ForMember(x => x.CreatedDate, opt => opt.Ignore())
    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
    .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
    .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<Sales, UpdateSalesCommand>()
  .ReverseMap()
  .ForMember(x => x.Ads, opt => opt.Ignore())
  .ForMember(x => x.CreatedDate, opt => opt.Ignore())
  .ForMember(x => x.CreatedBy, opt => opt.Ignore())
  .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
  .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<Sales, SalesUpdateResponse>()
   .ReverseMap()
   .ForMember(x => x.Ads, opt => opt.Ignore())
   .ForMember(x => x.CreatedDate, opt => opt.Ignore())
   .ForMember(x => x.CreatedBy, opt => opt.Ignore())
   .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
   .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsUpgrates, CreateUpgratesCommand>()
   .ReverseMap()
   .ForMember(x => x.Ads, opt => opt.Ignore())
   .ForMember(x => x.CreatedDate, opt => opt.Ignore())
   .ForMember(x => x.CreatedBy, opt => opt.Ignore())
   .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
   .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsUpgrates, UpgratesUpdateResponse>()
   .ReverseMap()
   .ForMember(x => x.Ads, opt => opt.Ignore())
   .ForMember(x => x.CreatedDate, opt => opt.Ignore())
   .ForMember(x => x.CreatedBy, opt => opt.Ignore())
   .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
   .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsUpgrates, UpdateUpgratesCommand>()
  .ReverseMap()
  .ForMember(x => x.Ads, opt => opt.Ignore())
  .ForMember(x => x.CreatedDate, opt => opt.Ignore())
  .ForMember(x => x.CreatedBy, opt => opt.Ignore())
  .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
  .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<AdsType, UpdatePropertyTypeCommand>()
 .ReverseMap()
 .ForMember(x => x.Ads, opt => opt.Ignore())
 .ForMember(x => x.CreatedDate, opt => opt.Ignore())
 .ForMember(x => x.CreatedBy, opt => opt.Ignore())
 .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
 .ForMember(x => x.LastModified, opt => opt.Ignore());



        }
    }
}
