using AutoMapper;
using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.SupplierOrders;

public class SupplierOrderProfile : Profile
{
    public SupplierOrderProfile()
    {
        CreateMap<CreateSupplierOrderCommand, SupplierOrderEntity>()
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.SupplierOrderMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.MessageId, opt => opt.Ignore())
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateSupplierOrderMaterialDto, SupplierOrderMaterialEntity>()
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.IsDelivered, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.SupplierOrder, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.StockTransaction, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierOrderId, opt => opt.Ignore());
    }
}