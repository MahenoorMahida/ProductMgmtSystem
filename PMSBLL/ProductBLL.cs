using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEntity;
using PMSException;
using PMSDAL;
using System.IO;

namespace PMSBLL
{
    public class ProductBLL
    {
        private static bool ValidateProduct(ProductEntity product)
        {
            StringBuilder sb = new StringBuilder();
            bool validProduct = true;
            if (product.ProductID <= 0)
            {
                validProduct = false;
                sb.Append(Environment.NewLine + "Invalid Product ID");

            }
            if (product.ProductName == string.Empty)
            {
                validProduct = false;
                sb.Append(Environment.NewLine + "Product Name Required");

            }
            if (product.ProductCategory == string.Empty)
            {
                validProduct = false;
                sb.Append(Environment.NewLine + "Product Category Required");
            }
            if (product.ProductPrice <= 0)
            {
                validProduct = false;
                sb.Append(Environment.NewLine + "Invalid Product Price");

            }
            if (validProduct == false)
                throw new ProductException(sb.ToString());
            return validProduct;
        }

        public static bool AddProductBLL(ProductEntity product)
        {
            bool productAdded = false;
            try
            {
                if (ValidateProduct(product))
                {
                    productAdded = ProductDAL.AddProductDAL(product);
                }
            }
            catch (ProductException)
            {
                throw;
            }

            return productAdded;
        }

        public static List<ProductEntity> ShowDetailsBLL()
        {
            List<ProductEntity> pmsList = null;
            try
            {
                pmsList = ProductDAL.ShowDetailsDAL();
            }
            catch (ProductException ex)
            {
                throw ex;
            }

            return pmsList;
        }

        public static bool DeleteBL(int deleteID)
        {
            bool Deleted = false;
            try
            {
                if (deleteID > 0)
                {
                    Deleted = ProductDAL.DeleteProductDAL(deleteID);
                }
                else
                {
                    throw new ProductException("Invalid Patient ID");
                }
            }
            catch (ProductException)
            {
                throw;
            }
            return Deleted;
        }

        public static ProductEntity SearchBL(int searchID)
        {
            ProductEntity search = null;
            try
            {
                search = ProductDAL.SearchDAL(searchID);
            }
            catch (ProductException ex)
            {
                throw ex;
            }
            return search;

        }

        public static bool UpdateBL(ProductEntity update)
        {
            bool Updated = false;
            try
            {
                if (ValidateProduct(update))
                {
                    ProductDAL productDAL = new ProductDAL();
                    Updated = ProductDAL.UpdateDAL(update);
                }
            }
            catch (ProductException)
            {
                throw;
            }
            return Updated;
        }

        public static void SetSerialization()
        {

            if (ProductDAL.PMSList != null)
            {
                ProductDAL.SetSerialization();
            }
        }
        public static void Setlist()
        {
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\" + ProductDAL.fileName))
                    ProductDAL.SetList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
