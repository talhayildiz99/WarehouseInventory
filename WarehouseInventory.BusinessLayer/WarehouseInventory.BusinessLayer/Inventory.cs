using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseInventory.DataLayer;

namespace WarehouseInventory.BusinessLayer
{
    public class Inventory
    {
        private DataLayer.DbInventoryEntities Context;
        public Inventory()
        {
            Context = new DataLayer.DbInventoryEntities();
        }

        public bool Add(TblInventories Inventory, out string Message)
        {
            bool result = false;
            Message = "";
            try
            {
                Inventory.InventoryGuid = Guid.NewGuid();
                Context.TblInventories.Add(Inventory);
                Context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public List<TblInventories> Get()
        {
            return (from data in Context.TblInventories select data).ToList();
        }

        public TblInventories Get(Guid InventoryGuid)
        {
            return (from data in Context.TblInventories
                    where data.InventoryGuid == InventoryGuid
                    select data).FirstOrDefault();
        }

        public List<TblInventories> Get(int WareHouseId)
        {
            return (from data in Context.TblInventories
                    where data.WarehousesId == WareHouseId
                    select data).ToList();
        }

        public bool Delete(Guid InventoryGuid, out string Message)
        {
            bool result = false;
            Message = "";
            try
            {
                Context.Database.
                    ExecuteSqlCommand("delete from TblInventories " +
                    "where InventoryGuid='" + InventoryGuid.ToString() + "'");
                result = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public bool Update(TblInventories Inventory, out string Message)
        {
            bool result = false;
            Message = "";
            try
            {
                TblInventories SelectedData = (from data in Context.TblInventories
                                               where data.InventoryGuid == Inventory.InventoryGuid
                                               select data).SingleOrDefault();
                if (SelectedData != null)
                {
                    SelectedData.WarehousesId = Inventory.WarehousesId;
                    SelectedData.InventoryName = Inventory.InventoryName;
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;

        }

    }
}