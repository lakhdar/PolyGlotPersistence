using Application.BoundedConext.ERPModule;
using ASP.NET.MVC5.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP.NET.MVC5.Client.Extensions;

namespace ASP.NET.MVC5.Client.Controllers
{
    public class FacilityController : Controller
    {
        private IOrganizationManagementService _organizationManagementService;
        public FacilityController(IOrganizationManagementService organizationManagementService)
        {
            if (organizationManagementService == (IOrganizationManagementService)null)
                throw new ArgumentNullException("organizationManagementService");
            _organizationManagementService = organizationManagementService;

        }

        // GET: Facility
        public async Task<ActionResult> Index()
        {

            var model = await _organizationManagementService.GetAllFacilitiesAsync();
            return View(model.ToViewModel());
        }

        // GET: Facility/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Facility/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facility/Create
        [HttpPost]
        public async Task<ActionResult> Create(FacilityCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var org = model.ToModel();
                    await _organizationManagementService.AddAsync(org);
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

        // GET: Facility/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Facility/Edit/5
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

        // GET: Facility/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Facility/Delete/5
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
