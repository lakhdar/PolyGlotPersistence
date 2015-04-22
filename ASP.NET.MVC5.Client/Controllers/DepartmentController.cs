using Application.BoundedConext.ERPModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP.NET.MVC5.Client.Extensions;
using ASP.NET.MVC5.Client.Models;

namespace ASP.NET.MVC5.Client.Controllers
{
    public class DepartmentController : Controller
    {

        private IDepartmentAggregateManagementService _departmentAggregateManagementService;
        private IOrganizationManagementService _organizationManagementService;
        public DepartmentController(IDepartmentAggregateManagementService departmentAggregateManagementService, IOrganizationManagementService organizationManagementService)
        {
            if (departmentAggregateManagementService == (IDepartmentAggregateManagementService)null)
                throw new ArgumentNullException("departmentAggregateManagementService");
            if (organizationManagementService == (IOrganizationManagementService)null)
                throw new ArgumentNullException("organizationManagementService");
            _departmentAggregateManagementService = departmentAggregateManagementService;
            _organizationManagementService = organizationManagementService;


        }



        // GET: Department
        public async Task<ActionResult> Index()
        {
            var model = await _departmentAggregateManagementService.GetAllDepartmentsAsync();
            return View(model.ToViewModel());
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            try
            {
                DepartmentCreateViewModel model = new DepartmentCreateViewModel();
                var fmodel = await _organizationManagementService.GetAllFacilitiesAsync();
                if (fmodel != null)
                {
                    model.FacilitiesModel = fmodel.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString() + "#$#" + x.Name.Replace(" ", "#+#"),
                        Text = x.Name
                    });
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }


        // POST: Department/Create
        [HttpPost]
        public async Task<ActionResult> Create(DepartmentCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dep = model.ToModel();
                    await this._departmentAggregateManagementService.AddDepartmentAsync(dep);
                    return RedirectToAction("Index");
                }

                return View(model);
                // TODO: Add insert logic here
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
