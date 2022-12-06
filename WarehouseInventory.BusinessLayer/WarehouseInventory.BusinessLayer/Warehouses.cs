using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WarehouseInventory.DataLayer;

namespace WarehouseInventory.BusinessLayer
{
    public class Warehouses
    {
        private DbInventoryEntities Context;

        public Warehouses()
        {
            Context = new DbInventoryEntities();
        }
        public bool Add(TblWarehouses warehouse, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                if (warehouse != null)
                {
                    warehouse.WarehouseGuid = Guid.NewGuid();
                    Context.TblWarehouses.Add(warehouse);
                    Context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return result;
        }

        public bool Update(TblWarehouses warehouse, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                if (warehouse != null)
                {
                    TblWarehouses SelectedData = (from data in Context.TblWarehouses
                                                  where data.WarehouseGuid == warehouse.WarehouseGuid
                                                  select data).FirstOrDefault();
                    if (SelectedData != null)
                    {
                        SelectedData.WarehouseName = warehouse.WarehouseName;
                        Context.SaveChanges();
                        result = true;
                    }
                                        
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return result;
        }

        public List<TblWarehouses> Get()
        {
            return (from data in Context.TblWarehouses
                    orderby data.WarehouseName
                    select data).ToList();
        }

        public TblWarehouses Get(Guid WarehouseGuid)
        {
            return (from data in Context.TblWarehouses
                    where data.WarehouseGuid.Value == WarehouseGuid
                    select data).SingleOrDefault();
        }

        public bool Delete(Guid WarehouseGuid, out string Message)
        {
            bool result = false;
            Message = "";
            try
            {
                Context.Database.
                    ExecuteSqlCommand("delete from TblWarehouses " +
                    "where WarehouseGuid='" + WarehouseGuid.ToString() + "'");
                result = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

    }
}
