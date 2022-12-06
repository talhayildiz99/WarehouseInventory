using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseInventory.BusinessLayer;
using WarehouseInventory.DataLayer;

namespace WarehouseInventory.Controllers
{
    public class WarehousesController : Controller
    {
        private Warehouses warehouses;
        public WarehousesController()
        {
            warehouses = new Warehouses();
        }
        public ActionResult Index()
        {
            ViewBag.WarehouseList = warehouses.Get();
            return View(ViewBag);
        }

        public ActionResult Detail(string WarehouseGuid)
        {
            bool Converted;
            Guid TmpGuid;
            Converted = Guid.TryParse(WarehouseGuid, out TmpGuid);
            if (Converted)
                ViewBag.Detail = warehouses.Get(TmpGuid);
            else
                Response.Redirect("~/", true);

            return View(ViewBag);
        }


        public ActionResult Add()
        {
            if (Request.HttpMethod == "POST")
            {
                string Message;
                string WarehouseName = Request.Form["TxtWarehouseName"].ToString();
                TblWarehouses warehouse = new TblWarehouses();
                warehouse.WarehouseGuid = Guid.NewGuid();
                warehouse.WarehouseName = WarehouseName;
                bool Success = warehouses.Add(warehouse, out Message);

                if (Success)
                    Response.Redirect("~/Warehouses", true);
                else
                    ViewBag.Message = Message;

            }
            return View();
        }

    }
}