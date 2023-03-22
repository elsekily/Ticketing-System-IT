using AutoMapper;
using TicketingSystemIT.Entities.Models;
using TicketingSystemIT.Entities.Resources;

namespace TicketingSystemIT.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Domain to API
        CreateMap<Category, CategoryResource>()
            .ForMember(c => c.EstimatedTimeInMinutes, opt => opt.MapFrom(cr => cr.EstimatedTimeInMinutes.TotalMinutes));

        CreateMap<Ticket, TicketForEmployeeWithCategoryResource>()
            .ForMember(tfer => tfer.IsAssigned, opt => opt.MapFrom(t => t.TimeAssigned == null ? false : true))
            .ForMember(tfer => tfer.CategoryName, opt => opt.MapFrom(t => t.Category.Name))
            .ForMember(tfer => tfer.TimeSolved, opt => opt.MapFrom(t => t.Category.Name))
            .AfterMap((t, tfer) =>
            {
                if (t.TimeAssigned == null)
                {
                    tfer.AssignedEmployeeName = "";
                    tfer.IsAssigned = false;
                }
                else
                {
                    tfer.AssignedEmployeeName = t.AssignedEmployee.UserName;
                    tfer.IsAssigned = true;
                }
            });

        //API to Domain

        CreateMap<CategorySaveResource, Category>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(c => c.Tickets, opt => opt.Ignore())
            .ForMember(c => c.Name, opt => opt.MapFrom(csr => csr.Name))
            .ForMember(c => c.EstimatedTimeInMinutes, opt => opt.MapFrom(csr => new TimeSpan(0, csr.EstimatedTimeInMinutes, 0)));



        CreateMap<TicketSaveResource, Ticket>()
           .ForMember(r => r.Id, opt => opt.Ignore())
           .ForMember(r => r.TimeIssued, opt => opt.MapFrom(tsr => DateTime.Now))
           .ForMember(r => r.TimeAssigned, opt => opt.Ignore())
           .ForMember(r => r.TimeSolved, opt => opt.Ignore())
           .ForMember(r => r.UserIssuedId, opt => opt.Ignore())
           .ForMember(r => r.UserIssued, opt => opt.Ignore())
           .ForMember(r => r.AssignedEmployee, opt => opt.Ignore())
           .ForMember(r => r.AssignedEmployeeID, opt => opt.Ignore())
           .ForMember(r => r.UserIssued, opt => opt.Ignore());

    }
}
