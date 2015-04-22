using ASP.NET.MVC5.Client.Models;
using Domain.BoundedContext.ERPModule;
using Domain.BoundedContext.StoreModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ASP.NET.MVC5.Client.Extensions
{
    public static class ModelExtentions
    {
        public static FacilityListViewModel ToViewModel(this IEnumerable<Organization> orgs)
        {
            FacilityListViewModel viewModel = null;
            if (orgs != null)
            {
                viewModel = new FacilityListViewModel()
                {
                    Total = orgs.Count(),
                    Facilities = orgs.ToList().Select(x => new FacilityItelViewModel()
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        Address = x.Address.AddressLine1 + x.Address.AddressLine2,
                        City = x.Address.City,
                        ZipCode = x.Address.ZipCode,

                    }),
                };
            }

            return viewModel;
        }

        public static Organization ToModel(this FacilityCreateViewModel model)
        {
            Organization entity = null;
            if (model != null)
            {
                entity = new Organization()
                {

                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Address = new Address()
                    {
                        Id=Guid.NewGuid(),
                        AddressLine1 = model.Address,
                        City = model.City,
                        ZipCode=model.ZipCode
                    }
                };
            }

            return entity;
        }


        public static EquipmentListViewModel ToViewModel(this IEnumerable<EquipmentAggregate> eqpmnts)
        {
            EquipmentListViewModel viewModel = null;
            if (eqpmnts != null)
            {
                viewModel = new EquipmentListViewModel()
                {
                    Total = eqpmnts.Count(),
                    Equipment = eqpmnts.Select(x => new EquipmentItemViewModel()
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        EquipmentModel = x.Model,
                        CreationDate = x.CreationDate.ToString(),
                        Description = x.Description,
                        DepartmentName=x.DepartmentName,
                    }),
                };
            }

            return viewModel;
        }


        public static EquipmentAggregate ToModel(this EquipmentCreateViewModel model)
        {
            EquipmentAggregate entity = null;
            if (model != null)
            {
                var ids= Regex.Split(model.DepartmentId, @"#\$#").ToList();
                string fId = ids[0];
                string fName = "";
                if (ids.Count() > 1)
                {
                    fName = ids[1];
                }
                entity = new EquipmentAggregate()
                {

                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Model = model.EquipmentModel,
                    DepartmentId =new  Guid(fId),
                    DepartmentName=fName.Replace("#+#"," "),
                    Description=model.Description,
                };
            }

            return entity;
        }

        public static DepartmentListViewModel ToViewModel(this IEnumerable<DepartmentAggregate> deps)
        {
            DepartmentListViewModel viewModel = null;
            if (deps != null)
            {
                viewModel = new DepartmentListViewModel()
                {
                    Total = deps.Count(),
                    Departments = deps.Select(x => new DepartmentemViewModel()
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        Description = x.Description,
                        FacilityName = x.FacilityName,
                    }),
                };
            }

            return viewModel;
        }


        public static DepartmentAggregate ToModel(this DepartmentCreateViewModel model)
        {
            DepartmentAggregate entity = null;
            if (model != null)
            {
                var ids = Regex.Split(model.FacilityId, @"#\$#").ToList();
                string fId = ids[0];
                string fName = "";
                if (ids.Count() > 1)
                {
                    fName = ids[1];
                }
                entity = new DepartmentAggregate()
                {

                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    FacilityId = new Guid(fId),
                    FacilityName = fName,
                    Description = model.Description,
                };
            }

            return entity;
        }

    }
}
