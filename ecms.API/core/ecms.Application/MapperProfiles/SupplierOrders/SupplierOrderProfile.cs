using AutoMapper;
using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Application.Models.ViewModels.SupplierOrders;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.SupplierOrders;

public class SupplierOrderProfile : Profile
{
    public SupplierOrderProfile()
    {
        CreateMap<CreateSupplierOrderCommand, SupplierOrderEntity>()
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.EditDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierOrderMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.MessageId, opt => opt.Ignore())
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierContact, opt => opt.Ignore())
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

        CreateMap<SupplierOrderEntity, SupplierOrderDto>()
            .ForMember(dest => dest.SupplierOrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.SupplierContactId, opt => opt.MapFrom(src => src.SupplierContactId))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.SendMessage, opt => opt.MapFrom(src => src.MessageId != null))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.MapFrom(src => src.CreateDateTimeUtc))
            .ForMember(dest => dest.EditDateTimeUtc, opt => opt.MapFrom(src => src.EditDateTimeUtc))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

        CreateMap<SupplierOrderMaterialEntity, SupplierOrderMaterialDto>()
            .ForMember(dest => dest.IsDelivered, opt => opt.MapFrom(src => src.IsDelivered))
            .ForMember(dest => dest.SupplierOrderMaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SupplierOrderId, opt => opt.MapFrom(src => src.SupplierOrderId))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material))
            .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<SupplierOrderEntity, SupplierOrderDetailsViewModel>()
            .ForMember(dest => dest.SupplierOrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.SendMessage, opt => opt.MapFrom(src => src.MessageId != null))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.MapFrom(src => src.CreateDateTimeUtc))
            .ForMember(dest => dest.EditDateTimeUtc, opt => opt.MapFrom(src => src.EditDateTimeUtc))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
            .ForMember(dest => dest.SupplierOrderMaterialDtos, opt => opt.MapFrom(src => src.SupplierOrderMaterials))
            .ForMember(dest => dest.SupplierContactDto, opt => opt.MapFrom(src => src.SupplierContact))
            .ForMember(dest => dest.SupplierDto, opt => opt.MapFrom(src => src.Supplier));

        CreateMap<EditSupplierOrderCommand, SupplierOrderEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SupplierOrderId))
            .ForMember(dest => dest.SupplierOrderMaterials, opt => opt.MapFrom(src => src.EditSupplierOrderMaterialDtos))
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.SupplierContactId, opt => opt.MapFrom(src => src.SupplierContactId))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.EditDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.MessageId, opt => opt.Ignore())
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierContact, opt => opt.Ignore());

        CreateMap<EditSupplierOrderMaterialDto, SupplierOrderMaterialEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SupplierOrderMaterialId))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.IsDelivered, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierOrder, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.StockTransaction, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierOrderId, opt => opt.Ignore());
    }
}