using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseInventory.BusinessLayer;
using WarehouseInventory.DataLayer;

namespace WarehouseInventory.Controllers
{
    public class InventoryController : Controller
    {
        BusinessLayer.Warehouses warehouses;
        BusinessLayer.Inventory inventory;
        public InventoryController()
        {
            warehouses = new Warehouses();
        }
        public ActionResult Index()
        {
            ViewBag.Warehouses = warehouses.Get();
            inventory = new Inventory();
            ViewBag.InventoryList = inventory.Get();
            if (Request.QueryString["CmbWarehouse"] != null)
            {
                if (Request.QueryString["CmbWarehouse"].ToString() != "")
                {
                    int WareHouseId;
                    bool Converted = int.TryParse(Request.QueryString["CmbWarehouse"].ToString(), out WareHouseId);
                    if (Converted)
                        ViewBag.InventoryList = inventory.Get(WareHouseId);
                    else
                        ViewBag.InventoryList = inventory.Get();

                }
            }

            return View(ViewBag);
        }
        public ActionResult AddInventory()
        {
            ViewBag.Warehouses = warehouses.Get();
            if (Request.HttpMethod == "GET")
            {
                return View(ViewBag);
            }
            else if (Request.HttpMethod == "POST")
            {
                string Warehouse = Request.Form["CmbWarehouse"];
                string InventoryName = Request.Form["TxtInventoryName"];
                if (Warehouse == "")
                {
                    ViewBag.Error = "Depo seçin";
                    return View(ViewBag);
                }
                else if (InventoryName == "")
                {
                    ViewBag.Error = "Ürün adı belirtin";
                    return View(ViewBag);
                }
                else
                {
                    inventory = new Inventory();
                    TblInventories tblInventories = new TblInventories();
                    tblInventories.InventoryName = InventoryName;
                    tblInventories.WarehousesId = int.Parse(Warehouse);
                    string Message;
                    bool Success = inventory.Add(tblInventories, out Message);
                    ViewBag.Success = Success;
                    if (!Success)
                        ViewBag.Error = Message;
                    return View(ViewBag);
                }

            }
            else
            {
                return View(ViewBag);
            }
        }

        public ActionResult InventoryDetail(string InventoryGuid)
        {
            inventory = new Inventory();
            Guid LInventoryGuid;
            bool Converted = Guid.TryParse(InventoryGuid, out LInventoryGuid);
            if (Converted)
                ViewBag.InventoryDetail = inventory.Get(LInventoryGuid);
            return View();
        }

        //[HttpPost]
        //public ActionResult Add()
        //{
        //    string Warehouse = Request.Form["CmbWarehouse"];
        //    string InventoryName = Request.Form["TxtInventoryName"];
        //    if (Warehouse == "")
        //    {
        //        ViewBag.Hata = "Depo seçin";
        //        Response.Redirect("~/Inventory/AddInventory", true);
        //        return null;
        //    }
        //    else if (InventoryName == "")
        //    {
        //        ViewBag.Hata = "Ürün adı belirtin";
        //        Response.Redirect("~/Inventory/AddInventory", true);
        //        return null;
        //    }
        //    else
        //    {
        //        inventory = new Inventory();
        //        TblInventories tblInventories = new TblInventories();
        //        tblInventories.InventoryName = InventoryName;
        //        tblInventories.WarehousesId = int.Parse(Warehouse);
        //        string Message;
        //        bool Success = inventory.Add(tblInventories, out Message);
        //        ViewBag.Success = Message;
        //        Response.Redirect("~/Inventory/AddInventory", true);
        //        return null;
        //    }
        //}
    }
}