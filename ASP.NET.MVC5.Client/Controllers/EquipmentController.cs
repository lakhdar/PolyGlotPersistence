using Application.BoundedConext.StoreModule;
using ASP.NET.MVC5.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP.NET.MVC5.Client.Extensions;
using Application.BoundedConext.ERPModule;

namespace ASP.NET.MVC5.Client.Controllers
{
    public class EquipmentController : Controller
    {

        private IEquipmentAggregateManagementService _equipmentAggregateManagementService;
        private IDepartmentAggregateManagementService _departmentAggregateManagementService;
        public EquipmentController(IEquipmentAggregateManagementService equipmentAggregateManagementService, IDepartmentAggregateManagementService departmentAggregateManagementService)
        {
            if (equipmentAggregateManagementService == (IEquipmentAggregateManagementService)null)
                throw new ArgumentNullException("organizationManagementService");
            if (departmentAggregateManagementService == (IDepartmentAggregateManagementService)null)
                throw new ArgumentNullException("departmentAggregateManagementService");
            _equipmentAggregateManagementService = equipmentAggregateManagementService;
            _departmentAggregateManagementService = departmentAggregateManagementService;


        }
        // GET: Equipment
        public async Task<ActionResult> Index()
        {


            var model = await _equipmentAggregateManagementService.GetAllEquipmentAsync();
            return View(model.ToViewModel());
        }

        // GET: Equipment/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Equipment/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                EquipmentCreateViewModel model = new EquipmentCreateViewModel();
                var fmodel = await _departmentAggregateManagementService.GetAllDepartmentsAsync();
                if (fmodel != null)
                {
                    model.FacilitiesModel = fmodel.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString() + "#$#" + x.Name.Replace(" ","#+#"),
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

        // POST: Equipment/Create
        [HttpPost]
        public async Task<ActionResult> Create(EquipmentCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var equip = model.ToModel();
                    await this._equipmentAggregateManagementService.AddEquipmentAsync(equip);
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

        // GET: Equipment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Equipment/Edit/5
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

        // GET: Equipment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Equipment/Delete/5
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
